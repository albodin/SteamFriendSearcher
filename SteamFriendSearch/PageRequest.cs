using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SteamFriendSearch
{
    /// <summary>
    /// Class that handles the Steam user search requests.
    /// </summary>
    public class PageRequest
    {
        private readonly CookieContainer cookieContainer = new CookieContainer();
        private readonly HttpClient httpClient;
        private string sessionId;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageRequest"/> class.
        /// </summary>
        /// <param name="userAgent">The user agent to use for requests.</param>
        public PageRequest(string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36")
        {
            if (httpClient == null)
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };
                httpClientHandler.CookieContainer = cookieContainer;
                httpClient = new HttpClient(httpClientHandler);
            }
            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd(userAgent);
            httpClient.BaseAddress = new Uri("https://steamcommunity.com/");
        }

        /// <summary>
        /// Gets the session ID for requests
        /// </summary>
        /// <returns>The session ID as a string</returns>
        private string GetSessionId()
        {
            // Send a request with an invalid sessionid value to get one from the received cookies
            HttpResponseMessage response = httpClient.GetAsync("/search/SearchCommunityAjax?text=user&filter=users&sessionid=NULLID&steamid_user=false&page=1").Result;
            string result = response.Content.ReadAsStringAsync().Result;

            // Find the sessionid cookie for future requests
            foreach (var cookie in from Cookie cookie in cookieContainer.GetCookies(new Uri("https://steamcommunity.com/"))
                                   where cookie.Name == "sessionid"
                                   select cookie)
            {
                return cookie.Value;
            }

            // Cookie wasn't found, so the request probably failed
            return null;
        }

        /// <summary>
        /// Asynchronously gets the users at some particular page
        /// </summary>
        /// <param name="userName">Name of user to search.</param>
        /// <param name="page">Page number to check.</param>
        /// <returns>List of the <see cref="User"/> class.</returns>
        public async Task<ResultPage> GetPageUsersAsync(string userName, int page)
        {
            if (sessionId == null)
            {
                sessionId = GetSessionId();
            }

            // Steam has a 500 page limit
            if (page > 500)
            {
                return new ResultPage(search_text: userName, search_page: page);
            }

            HttpResponseMessage response = httpClient.GetAsync($"/search/SearchCommunityAjax?text={userName}&filter=users&sessionid={sessionId}&steamid_user=false&page={page}").Result;
            if (!response.IsSuccessStatusCode)
            {
                return new ResultPage(search_text: userName, search_page: page);
            }

            string result = response.Content.ReadAsStringAsync().Result;
            ResultPage resultPage = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultPage>(result);

            if (JObject.Parse(result)["success"].ToString() != "1" || resultPage.Html.Contains("There are no users that match your search") || resultPage.Html.Contains("The search server did not understand your search term"))
            {
                resultPage.Success = false;
                return resultPage;
            }

            resultPage.Users = await ParseReturnHtmlAsync(resultPage.Html).ConfigureAwait(false);

            return resultPage;
        }

        /// <summary>
        /// Asynchronously downloads an image from some specified URL.
        /// </summary>
        /// <param name="url">The URL of the image.</param>
        /// <returns>Tuple of if boolean stating whether the URL was found and the <see cref="Image"/> class.</returns>
        private async Task<Tuple<bool, Image>> GetImageAsync(string url)
        {
            HttpResponseMessage response = httpClient.GetAsync(url).Result;
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new Tuple<bool, Image>(false, null);
            }
            if (!response.IsSuccessStatusCode)
            {
                return new Tuple<bool, Image>(true, null);
            }
            byte[] content = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            using (MemoryStream memoryStream = new MemoryStream(content))
            {
                return new Tuple<bool, Image>(true, new Bitmap(Image.FromStream(memoryStream)));
            }
        }

        /// <summary>
        /// Asynchronously parses the HTML content from the search response.
        /// </summary>
        /// <param name="html">The HTML string to parse.</param>
        /// <returns>List of the <see cref="User"/> class.</returns>
        private async Task<List<User>> ParseReturnHtmlAsync(string html)
        {
            List<User> users = new List<User>();
            var document = new HtmlDocument();
            document.LoadHtml(html);

            // Looping through all of the HtmlNodes that match our xpath, each "search_row" contains info for one user result
            foreach (HtmlNode user in document.DocumentNode.SelectNodes("//div[contains(@class, 'search_row')]"))
            {
                HtmlNode searchPersonaName = user.SelectSingleNode(".//a[@class='searchPersonaName'][1]");
                string username = searchPersonaName.InnerText;
                string profileUrl = searchPersonaName.GetAttributeValue("href", "NOTFOUND");
                bool customId = false; // Set to true if user set a custom URL ID
                string id;
                ulong steamId64 = 0;
                if (profileUrl.Contains("profiles"))
                {
                    id = profileUrl.Replace("https://steamcommunity.com/profiles/", "");
                    steamId64 = Convert.ToUInt64(id);
                }
                else
                {
                    id = profileUrl.Replace("https://steamcommunity.com/id/", "");
                    customId = true;
                }

                List<string> otherNames = new List<string>();
                HtmlNode akaUsers = user.SelectSingleNode(".//div[@class='search_match_info'][1]/div[2]"); // Node containing the other names the user goes by
                if (akaUsers != null)
                {
                    foreach (HtmlNode child in akaUsers.ChildNodes)
                    {
                        if (child.Name == "span")
                        {
                            otherNames.Add(child.InnerText);
                        }
                    }
                }

                string imageLoc = user.SelectSingleNode(".//a[1]/img[1]").GetAttributeValue("src", "NOTFOUND").Replace("medium", "full"); // Find the profile picture URL and replace "medium" with "full" to get the full size image
                Tuple<bool, Image> imageResult = null;
                if (imageLoc != "NOTFOUND")
                {
                    imageResult = await GetImageAsync(imageLoc).ConfigureAwait(false);
                }
                if (imageResult != null)
                {
                    users.Add(new User(username, id, steamId64, customId, imageResult.Item2, imageResult.Item1, otherNames));
                }
                else
                {
                    // Assume the profile picture URL is valid even if we don't have it
                    users.Add(new User(username, id, steamId64, customId, null, true, otherNames));
                }
            }

            return users;
        }
    }
}