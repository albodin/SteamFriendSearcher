using System.Collections.Generic;

namespace SteamFriendSearch
{
    /// <summary>
    /// Class to store Steam user search result
    /// </summary>
    public class ResultPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultPage"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> the request was successful.</param>
        /// <param name="search_text">The username searched.</param>
        /// <param name="search_result_count">Total users found.</param>
        /// <param name="search_filter">The search filter.</param>
        /// <param name="search_page">The page searched.</param>
        /// <param name="html">The HTML in the response.</param>
        /// <param name="users">List of the <see cref="User"/> class.</param>
        public ResultPage(bool success = false, string search_text = "", int search_result_count = 0, string search_filter = "", int search_page = 0, string html = "", List<User> users = null)
        {
            Success = success;
            SearchText = search_text;
            SearchResultCount = search_result_count;
            SearchFilter = search_filter;
            SearchPage = search_page;
            Html = html;
            Users = users;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ResultPage"/> request was successful.
        /// </summary>
        /// <value>
        ///   <c>true</c> if successful; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the search text (username).
        /// </summary>
        /// <value>
        /// The search text.
        /// </value>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets or sets the total users found.
        /// </summary>
        /// <value>
        /// The amount of users found.
        /// </value>
        public int SearchResultCount { get; set; }

        /// <summary>
        /// Gets or sets the search filter.
        /// Will always be "users" as we're searching for users.
        /// </summary>
        /// <value>
        /// The search filter.
        /// </value>
        public string SearchFilter { get; set; }

        /// <summary>
        /// Gets or sets the page number that was searched.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int SearchPage { get; set; }

        /// <summary>
        /// Gets or sets the full HTML in the JSON "html" key.
        /// </summary>
        /// <value>
        /// The full HTML.
        /// </value>
        public string Html { get; set; }

        /// <summary>
        /// Gets or sets the users found in this page.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public List<User> Users { get; set; }
    }
}