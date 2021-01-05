using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamFriendSearch;
using System.Threading;
using SteamWebAPI2.Utilities;
using SteamWebAPI2.Interfaces;
using System.Net.Http;
using Steam.Models.SteamCommunity;

namespace SteamFriendSearchApp
{
    /// <summary>
    /// Form to handle the user selections, searching, and processing.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FriendSearchForm : Form
    {
        private readonly AddRecentGameForm addRecentGameForm;
        private readonly Semaphore semaphore = new Semaphore(1, 1);
        private readonly Dictionary<string, List<User>> usersDic = new Dictionary<string, List<User>>();
        private readonly List<string> foundUserUrls = new List<string>();
        private readonly List<Task> taskList = new List<Task>();
        private int totalUsersSearching;
        private string currentUserSearch;
        private int usersProcessed;
        private Status status;

        private readonly SteamWebInterfaceFactory webInterfaceFactory = null;
        private readonly SteamUser steamInterface;
        private readonly PlayerService playerService;

        /// <summary>
        /// enum of possible status states
        /// </summary>
        private enum Status
        {
            /// <summary>
            /// Waiting to start
            /// </summary>
            None,

            /// <summary>
            /// Preparing downloading threads
            /// </summary>
            Preparing,

            /// <summary>
            /// Downloading search pages containing users
            /// </summary>
            DownloadingUsers,

            /// <summary>
            /// Final processing of user data
            /// </summary>
            ProcessingUsers
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendSearchForm"/> class.
        /// </summary>
        public FriendSearchForm()
        {
            addRecentGameForm = new AddRecentGameForm(this);
            if (Program.SteamApiKey != "")
            {
                webInterfaceFactory = new SteamWebInterfaceFactory(Program.SteamApiKey);
                steamInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUser>(new HttpClient());
                playerService = webInterfaceFactory.CreateSteamWebInterface<PlayerService>(new HttpClient());
            }

            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the FriendSearchForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FriendSearchForm_Load(object sender, EventArgs e)
        {
            userStatusComboBox.SelectedIndex = 0; // Default to ANY selection

            // Disable controls that rely on valid Steam API Key
            if (Program.SteamApiKey?.Length == 0)
            {
                gameInfoGroupBox.Enabled = false;
                userStatusGroupBox.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the profileAnyRadioButton control by enabling/disabling specific controls if the radio button becomes checked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void profileAnyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton?.Checked == true)
            {
                profilePictureBox.Visible = false;
                selectProfilePicButton.Visible = false;
                imageSimGroupBox.Visible = false;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the profileDefaultRadioButton control by enabling/disabling specific controls if the radio button becomes checked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void profileDefaultRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton?.Checked == true)
            {
                profilePictureBox.Image = Properties.Resources.default_profile_full;
                profilePictureBox.Visible = true;
                selectProfilePicButton.Visible = false;
                imageSimGroupBox.Visible = true;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the profileCustomRadioButton control by enabling/disabling specific controls if the radio button becomes checked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void profileCustomRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton?.Checked == true)
            {
                profilePictureBox.Visible = true;
                selectProfilePicButton.Visible = true;
                imageSimGroupBox.Visible = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the selectProfilePicButton control by asking the user to select an image.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void selectProfilePicButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.RestoreDirectory = true;
                string separator = ""; // separator starts as empty string so that the filter doesn't start with a vertical bar
                int index = 1;
                foreach (ImageCodecInfo codecInfo in ImageCodecInfo.GetImageEncoders())
                {
                    string codecName = codecInfo.CodecName.Substring(8).Replace("Codec", "Files").Trim(); // Start at index 8 to remove "Built-in" from the string
                    openFileDialog.Filter = string.Format("{0}{1}{2} ({3})|{3}", openFileDialog.Filter, separator, codecName, codecInfo.FilenameExtension);
                    separator = "|";

                    if (codecInfo.FilenameExtension.Contains("PNG"))
                    {
                        openFileDialog.FilterIndex = index;
                    }
                    index++;
                }
                openFileDialog.Filter = string.Format("{0}{1}{2} ({3})|{3}", openFileDialog.Filter, separator, "All Files", "*.*"); // Add "All Files" option
                openFileDialog.DefaultExt = ".PNG";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        profilePictureBox.Image = Image.FromFile(openFileDialog.FileName);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error loading file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Scroll event of the thresholdTrackBar control by updating the <see cref="thresholdLabel"/> text to the trackbar value.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void thresholdTrackBar_Scroll(object sender, EventArgs e)
        {
            thresholdLabel.Text = $"Threshold: {thresholdTrackBar.Value}%";
        }

        /// <summary>
        /// Handles the CheckedChanged event of the inGameCheckBox control by
        /// changing the visibility of specific controls based on the new checked value.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void inGameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (inGameCheckBox.Checked)
            {
                appIdLabel.Visible = true;
                appIdNumericUpDown.Visible = true;
            }
            else
            {
                appIdLabel.Visible = false;
                appIdNumericUpDown.Visible = false;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the recentGamesCheckBox control by making the visibility
        /// of <see cref="recentGamesListBox"/> the same as <see cref="recentGamesCheckBox"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void recentGamesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            recentGamesListBox.Visible = recentGamesCheckBox.Checked;
        }

        /// <summary>
        /// Handles the MouseUp event of the recentGamesListBox control by changing the
        /// visibility of <see cref="recentGamesContextMenuStrip"/> and its related controls.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void recentGamesListBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int clickIndex = recentGamesListBox.IndexFromPoint(e.Location);
                if (clickIndex != ListBox.NoMatches) // Click location is a valid index
                {
                    recentGamesListBox.SelectedIndex = clickIndex;
                    addToolStripMenuItem.Visible = true;
                    removeToolStripMenuItem.Visible = true;
                }
                else
                {
                    addToolStripMenuItem.Visible = true;
                    removeToolStripMenuItem.Visible = false;
                }
                recentGamesContextMenuStrip.Visible = true;
                recentGamesContextMenuStrip.Show(Cursor.Position);
            }
            else
            {
                recentGamesContextMenuStrip.Visible = false;
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the matchedUsersListBox control by changing the
        /// visibility of <see cref="foundUsersContextMenuStrip"/> and its related controls
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void matchedUsersListBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int clickIndex = matchedUsersListBox.IndexFromPoint(e.Location);
                if (clickIndex != ListBox.NoMatches) // Click location is a valid index
                {
                    matchedUsersListBox.SelectedIndex = clickIndex;
                    copySelectedToolStripMenuItem.Visible = true;
                }
                else
                {
                    copySelectedToolStripMenuItem.Visible = false;
                }
                foundUsersContextMenuStrip.Visible = true;
                foundUsersContextMenuStrip.Show(Cursor.Position);
            }
            else
            {
                foundUsersContextMenuStrip.Visible = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the copySelectedToolStripMenuItem control
        /// by copying the <see cref="matchedUsersListBox"/> selected item to the clipboard.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void copySelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(matchedUsersListBox.SelectedItem.ToString());
        }

        /// <summary>
        /// Handles the Click event of the copyAllToolStripMenuItem control
        /// by copying the <see cref="matchedUsersListBox"/> items to the clipboard.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (matchedUsersListBox.Items.Count == 0)
            {
                // Nothing to copy
                return;
            }

            string items = "";
            foreach (var url in matchedUsersListBox.Items)
            {
                items += url.ToString() + "\n";
            }
            Clipboard.SetText(items);
        }

        /// <summary>
        /// Handles the Click event of the addToolStripMenuItem control
        /// by showing the <see cref="addRecentGameForm"/> form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addRecentGameForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the removeToolStripMenuItem control
        /// by removing the selected item in the <see cref="recentGamesListBox"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            recentGamesListBox.Items.RemoveAt(recentGamesListBox.SelectedIndex);
        }

        /// <summary>
        /// Handles the MouseHover event of the compareUsernamesCheckBox control
        /// by providing a tool tip of what the <see cref="compareUsernamesCheckBox"/> control does.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void compareUsernamesCheckBox_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(compareUsernamesCheckBox, "If checked username will be used for searching and comparing");
        }

        /// <summary>
        /// Handles the MouseHover event of the compareAkaCheckBox control
        /// by providing a tool tip of what the <see cref="compareAkaCheckBox"/> control does.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void compareAkaCheckBox_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(compareAkaCheckBox, "If checked will also compare to AKA usernames");
        }

        /// <summary>
        /// Handles the CheckedChanged event of the compareUsernamesCheckBox control
        /// by changing the visibility of the <see cref="caseSensitiveCheckBox"/> and
        /// <see cref="compareAkaCheckBox"/> controls based on the checked state of
        /// the compareUsernamesCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void compareUsernamesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (compareUsernamesCheckBox.Checked)
            {
                caseSensitiveCheckBox.Visible = true;
                compareAkaCheckBox.Visible = true;
            }
            else
            {
                caseSensitiveCheckBox.Visible = false;
                compareAkaCheckBox.Visible = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the StartButton control by disabling the form to prevent
        /// further changes, and then start the downloading and processing of the search request
        /// based on selections in the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void StartButton_Click(object sender, EventArgs e)
        {
            status = Status.Preparing;

            flowLayoutPanel3.Enabled = false;
            matchedUsersListBox.Items.Clear();
            foundUserUrls.Clear();
            usersProcessed = 0;

            string username = usernameTextBox.Text;
            currentUserSearch = username;
            int threads = threadsTrackBar.Value;
            statusTimer.Start();

            // Download users if it hasn't been done already, or the user wants to re-download
            if (!usersDic.ContainsKey(username) || MessageBox.Show($"The username \"{username}\" has already been searched, would you like to use the cached information?", "Username Already Searched", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                usersDic.Remove(username);
                await Task.Run(() => startRequestsAsync(username, threads)).ConfigureAwait(false);
            }

            // Temp vars to send to process method
            int imageSimilarityThreshold = 0;
            bool compareUsername = false;
            bool caseSensitiveUsername = false;
            bool compareAkaNames = false;
            bool comparePfp = false;
            Image compareImage = null;
            int userStatusSelectedIndex = 0;
            bool checkIngame = false;
            string ingameId = null;
            bool checkRecentGames = false;
            List<string> recentGamesList = null;
            bool tempProfileDefChecked = false;
            bool tempProfileCusChecked = false;

            // Safely get the values from the form as this method will be running in a separate thread
            Invoke(new MethodInvoker(() => imageSimilarityThreshold = thresholdTrackBar.Value));
            Invoke(new MethodInvoker(() => compareUsername = compareUsernamesCheckBox.Checked));
            Invoke(new MethodInvoker(() => caseSensitiveUsername = caseSensitiveCheckBox.Checked));
            Invoke(new MethodInvoker(() => compareAkaNames = compareAkaCheckBox.Checked));
            Invoke(new MethodInvoker(() => tempProfileDefChecked = profileDefaultRadioButton.Checked));
            Invoke(new MethodInvoker(() => tempProfileCusChecked = profileCustomRadioButton.Checked));
            Invoke(new MethodInvoker(() => userStatusSelectedIndex = userStatusComboBox.SelectedIndex));
            Invoke(new MethodInvoker(() => checkIngame = inGameCheckBox.Checked));
            Invoke(new MethodInvoker(() => ingameId = appIdNumericUpDown.Value.ToString()));
            Invoke(new MethodInvoker(() => checkRecentGames = recentGamesCheckBox.Checked));
            Invoke(new MethodInvoker(() => recentGamesList = recentGamesListBox.Items.Cast<string>().ToList()));

            if (tempProfileDefChecked || tempProfileCusChecked)
            {
                comparePfp = true;
                Invoke(new MethodInvoker(() => compareImage = profilePictureBox.Image));
            }

            // Start the processing thread
            await Task.Run(() => processUsers(username, imageSimilarityThreshold, compareUsername, caseSensitiveUsername, compareAkaNames, comparePfp, compareImage, userStatusSelectedIndex, checkIngame, ingameId, checkRecentGames, recentGamesList)).ConfigureAwait(false);

            // Add the matched user URLs to the listbox
            foreach (string url in foundUserUrls)
            {
                if (!matchedUsersListBox.Items.Contains(url))
                {
                    Invoke(new MethodInvoker(() => matchedUsersListBox.Items.Add(url)));
                }
            }

            // Re-enable the form
            Invoke(new MethodInvoker(() => flowLayoutPanel3.Enabled = true));
            status = Status.None;
        }

        /// <summary>
        /// Handles the Tick event of the statusTimer control
        /// by changing the text of the <see cref="statusLabel"/> control
        /// based on the current status.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void statusTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                switch (status)
                {
                    case Status.None:
                        statusLabel.Text = "Status: Waiting to start";
                        statusTimer.Stop();
                        break;

                    case Status.Preparing:
                        statusLabel.Text = "Status: Preparing to start";
                        break;

                    case Status.DownloadingUsers:
                        statusLabel.Text = $"Status: Saved {usersDic[currentUserSearch].Count}/{totalUsersSearching} users";
                        break;

                    case Status.ProcessingUsers:
                        statusLabel.Text = $"Status: Processed {usersProcessed}/{usersDic[currentUserSearch].Count} users";
                        break;
                }
            }
            catch { }
        }

        /// <summary>
        /// Enables the start button if the <see cref="usernameTextBox"/> text is valid
        /// and the <see cref="userStatusComboBox"/> selection is valid.
        /// </summary>
        private void checkAndEnableStartButton()
        {
            startButton.Enabled = (usernameTextBox.TextLength > 0 && userStatusComboBox.SelectedIndex != -1);
        }

        /// <summary>
        /// Handles the TextChanged event of the usernameTextBox control
        /// by calling <see cref="checkAndEnableStartButton"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void usernameTextBox_TextChanged(object sender, EventArgs e)
        {
            checkAndEnableStartButton();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the userStatusComboBox control
        /// by calling <see cref="checkAndEnableStartButton"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void userStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkAndEnableStartButton();
        }

        /// <summary>
        /// Handles the Scroll event of the threadsTrackBar control by updating the <see cref="threadsLabel"/> text to the trackbar value.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void threadsTrackBar_Scroll(object sender, EventArgs e)
        {
            threadsLabel.Text = $"Threads: {threadsTrackBar.Value}";
        }

        /// <summary>
        /// Asynchronously gets the steam ID from a vanity URL.
        /// </summary>
        /// <param name="url">The users Steam URL.</param>
        /// <returns>The users Steam ID</returns>
        private async Task<ulong> getSteamIdVanityAsync(string url)
        {
            while (true)
            {
                try
                {
                    var steamId = await steamInterface.ResolveVanityUrlAsync(url).ConfigureAwait(false);
                    return steamId.Data;
                }
                catch { }
            }
        }

        /// <summary>
        /// Processes the users in the <see cref="usersDic"/> dictionary and matches them based on the method parameters.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="similarityThreshold">The profile picture similarity threshold.</param>
        /// <param name="compareUsername">if set to <c>true</c> usernames will be compared.</param>
        /// <param name="caseSensitiveUsername">if set to <c>true</c> will compare usernames with case sensitivity enabled.</param>
        /// <param name="compareAkaNames">if set to <c>true</c> will compare to users other names.</param>
        /// <param name="comparePfp">if set to <c>true</c> will compare profile pictures.</param>
        /// <param name="compareImage">The image to which to compare to.</param>
        /// <param name="userStatusSelectedIndex">Index of the <see cref="userStatusComboBox"/> control.</param>
        /// <param name="checkIngame">if set to <c>true</c> will check if user is ingame and if game matches <see cref="ingameId"/>.</param>
        /// <param name="ingameId">The game ID to compare to.</param>
        /// <param name="checkRecentGames">if set to <c>true</c> will check users recently played games.</param>
        /// <param name="recentGamesList">The recent games list to compare to.</param>
        private async Task processUsers(string username, int similarityThreshold, bool compareUsername, bool caseSensitiveUsername,
            bool compareAkaNames, bool comparePfp, Image compareImage, int userStatusSelectedIndex, bool checkIngame, string ingameId, bool checkRecentGames, List<string> recentGamesList)
        {
            status = Status.ProcessingUsers;
            if (!usersDic.ContainsKey(username))
            {
                // Nothing to check
                return;
            }

            List<ulong> steamApiUsersQueue = new List<ulong>();
            foreach (User user in usersDic[username])
            {
                // Username text comparison
                if (compareUsername)
                {
                    bool foundMatch = false;
                    if (caseSensitiveUsername)
                    {
                        if (string.Equals(username, user.Username) || (user.CustomId && string.Equals(username, user.Id)))
                        {
                            foundMatch = true;
                        }
                    }
                    else
                    {
                        if (string.Equals(username, user.Username, StringComparison.CurrentCultureIgnoreCase) || (user.CustomId && string.Equals(username, user.Id, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            foundMatch = true;
                        }
                    }

                    // Compare to other names the user goes by
                    if (compareAkaNames)
                    {
                        foreach (string akaName in user.OtherNames)
                        {
                            if (caseSensitiveUsername)
                            {
                                if (string.Equals(akaName, user.Username))
                                {
                                    foundMatch = true;
                                }
                            }
                            else
                            {
                                if (string.Equals(akaName, user.Username, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    foundMatch = true;
                                }
                            }
                        }
                    }

                    // No username match, continue on
                    if (!foundMatch)
                    {
                        usersProcessed++;
                        continue;
                    }
                }

                // Profile picture comparison
                if (comparePfp)
                {
                    double similarity = getImageSimilarityPercentage(compareImage, user.ProfilePic);
                    if (similarity < similarityThreshold)
                    {
                        usersProcessed++;
                        continue;
                    }
                }

                // If no Steam API key then we finish here without continuing further on
                string userUrl = user.CustomId ? "https://steamcommunity.com/id/" + user.Id : "https://steamcommunity.com/profiles/" + user.Id;
                if (webInterfaceFactory == null)
                {
                    foundUserUrls.Add(userUrl);
                    usersProcessed++;
                    continue;
                }

                // If true further processing needs to happen, so we add all ID's
                // to a list to coalesce requests which saves time and API requests
                if (userStatusSelectedIndex != 0 || checkIngame || checkRecentGames)
                {
                    if (user.SteamId == 0)
                    {
                        user.SteamId = await getSteamIdVanityAsync(user.Id).ConfigureAwait(false);
                    }

                    steamApiUsersQueue.Add(user.SteamId);
                }
            }

            // GetPlayerSummaries has a limit of 100 steam Ids so the list needs to be split
            while (steamApiUsersQueue.Count > 0)
            {
                var tempSteamQueue = steamApiUsersQueue.Take(100).ToList();

                IReadOnlyCollection<PlayerSummaryModel> playerSummaries = null;
                while (playerSummaries == null)
                {
                    try
                    {
                        var playerSummariesResponse = await steamInterface.GetPlayerSummariesAsync(tempSteamQueue).ConfigureAwait(false);
                        playerSummaries = playerSummariesResponse.Data;
                    }
                    catch { }
                }

                foreach (var playerSummary in playerSummaries)
                {
                    // User status comparison
                    // Offline state value is 0 while userStatusComboBox SelectedIndex will be 1 for offline
                    // so we subtract 1 to align the values
                    if (userStatusSelectedIndex != 0 && (int)playerSummary.UserStatus != (userStatusSelectedIndex - 1))
                    {
                        usersProcessed++;
                        continue;
                    }

                    // User in game
                    if (checkIngame && playerSummary.PlayingGameId != ingameId)
                    {
                        usersProcessed++;
                        continue;
                    }

                    // User recent games played
                    if (checkRecentGames)
                    {
                        RecentlyPlayedGamesResultModel recentlyPlayedGameResult = null;
                        while (true)
                        {
                            try
                            {
                                var playerRecentlyPlayedResponse = await playerService.GetRecentlyPlayedGamesAsync(playerSummary.SteamId).ConfigureAwait(false);
                                recentlyPlayedGameResult = playerRecentlyPlayedResponse.Data;
                                break;
                            }
                            catch { }
                        }

                        List<string> recentlyPlayedList = new List<string>();
                        foreach (var recentGame in recentlyPlayedGameResult.RecentlyPlayedGames)
                        {
                            recentlyPlayedList.Add(recentGame.AppId.ToString());
                        }

                        if (!recentGamesList.All(i => recentlyPlayedList.Contains(i)))
                        {
                            usersProcessed++;
                            continue;
                        }
                    }

                    foundUserUrls.Add(playerSummary.ProfileUrl);
                }

                steamApiUsersQueue = steamApiUsersQueue.Skip(100).ToList();
            }
        }

        /// <summary>
        /// Adds the users to the main users dictionary <see cref="usersDic"/>.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="users">The users to add.</param>
        private void addUsers(string username, List<User> users)
        {
            if (usersDic.TryGetValue(username, out List<User> existingList))
            {
                existingList.AddRange(users);
            }
            else
            {
                usersDic.Add(username, users);
            }
        }

        /// <summary>
        /// Asynchronously gets the users in the specified pages.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="pages">The pages to check.</param>
        private async Task getUsersAsync(string username, List<int> pages)
        {
            PageRequest pageRequest = new PageRequest();
            for (int i = 0; i < pages.Count;)
            {
                ResultPage resultPage = await pageRequest.GetPageUsersAsync(username, pages[i]).ConfigureAwait(false);
                if (resultPage.Success)
                {
                    bool goodResult = true;
                    foreach (User user in resultPage.Users)
                    {
                        if (!user.ValidProfilePicUrl)
                        {
                            user.ProfilePic = Properties.Resources.default_profile_full;
                        }
                        if (user.ProfilePic == null || user.Id == "NOTFOUND")
                        {
                            goodResult = false;
                        }
                    }

                    // If the result was satisfactory then we
                    // add the users in the page to the rest of
                    // of the users found, and increment i to continue
                    if (goodResult)
                    {
                        semaphore.WaitOne();
                        addUsers(username, resultPage.Users);
                        semaphore.Release();
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// Asynchronously Starts the requests to download the users from the search pages.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="threads">The max thread count.</param>
        private async Task startRequestsAsync(string username, int threads)
        {
            status = Status.Preparing;
            PageRequest pageRequest = new PageRequest();
            ResultPage initialRequest = null;
            while (initialRequest?.Success != true)
            {
                initialRequest = await pageRequest.GetPageUsersAsync(username, 1).ConfigureAwait(false);
            }

            // Limit of 500 pages or 10,000 users. If less than that then we divide user count by users per page (20)
            int pages = initialRequest.SearchResultCount > 10000 ? 500 : (int)Math.Ceiling((double)(initialRequest.SearchResultCount / 20.0));
            int pagesPerThread = pages <= threads ? 1 : pages / threads;
            totalUsersSearching = initialRequest.SearchResultCount > 10000 ? 10000 : initialRequest.SearchResultCount;

            List<List<int>> pagesLists = new List<List<int>>();
            List<int> tempPages = new List<int>();
            for (int i = 1; i <= pages; i++)
            {
                if (tempPages.Count < pagesPerThread)
                {
                    tempPages.Add(i);
                }
                if (tempPages.Count >= pagesPerThread)
                {
                    pagesLists.Add(new List<int>(tempPages));
                    tempPages.Clear();
                }
            }
            pagesLists[0].AddRange(new List<int>(tempPages)); // Add rest of pages to first list

            status = Status.DownloadingUsers;
            foreach (var pageList in pagesLists)
            {
                var task = new Task(async () => await getUsersAsync(username, pageList).ConfigureAwait(false));
                taskList.Add(task);
                task.Start();
            }
            Task.WaitAll(taskList.ToArray());
        }

        /// <summary>
        /// Basic method the get the similarity of two images
        /// </summary>
        /// <param name="image1">The first image.</param>
        /// <param name="image2">The second image.</param>
        /// <returns>Double from 0-100 with 0 being no similarity in images and 100 being both images are the same</returns>
        private double getImageSimilarityPercentage(Image image1, Image image2)
        {
            Image tempImage1 = new Bitmap(image1);
            Image tempImage2 = new Bitmap(image2);

            // Images should be the same size
            if ((tempImage1.Width * tempImage1.Height) > (tempImage2.Width * tempImage2.Height))
            {
                tempImage1.Dispose();
                tempImage1 = new Bitmap(image1, new Size(tempImage2.Width, tempImage2.Height));
            }
            else if ((tempImage1.Width * tempImage1.Height) < (tempImage2.Width * tempImage2.Height))
            {
                tempImage2.Dispose();
                tempImage2 = new Bitmap(image2, new Size(tempImage1.Width, tempImage1.Height));
            }

            var img1 = ((Bitmap)tempImage1).ToImage<Bgra, byte>();
            var img2 = ((Bitmap)tempImage2).ToImage<Bgra, byte>();
            var cmpMask = img1.Cmp(img2, CmpType.Equal);
            int[] matches = cmpMask.CountNonzero();
            int total = 0;
            foreach (int match in matches)
            {
                total += match;
            }

            double avg = total / matches.Length;
            double imgSize = tempImage1.Width * tempImage1.Height;
            double similarity = avg / imgSize;

            tempImage1.Dispose();
            tempImage2.Dispose();
            img1.Dispose();
            img2.Dispose();
            cmpMask.Dispose();
            GC.Collect();
            return similarity * 100;
        }

        /// <summary>
        /// Adds a game ID to the <see cref="recentGamesListBox"/> control.
        /// </summary>
        /// <param name="gameId">The game ID.</param>
        public void addRecentGameId(string gameId)
        {
            recentGamesListBox.Items.Add(gameId);
        }
    }
}