using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace SteamFriendSearchApp
{
    /// <summary>
    /// Form to handle the setting of the Steam API key
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class SteamApiForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SteamApiForm"/> class.
        /// </summary>
        public SteamApiForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the doneButton control.
        /// Makes sure that the Steam API key is valid and closes this form to allow the next form to open.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void doneButton_Click(object sender, EventArgs e)
        {
            try
            {
                SteamWebInterfaceFactory webInterfaceFactory = new SteamWebInterfaceFactory(apiKeyTextBox.Text);
                SteamUser steamInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUser>(new HttpClient());
                await steamInterface.GetPlayerSummaryAsync(76561197977699530).ConfigureAwait(false); // SteamId of https://steamcommunity.com/id/valve
                Program.SteamApiKey = apiKeyTextBox.Text;
                Invoke(new MethodInvoker(() => Close()));
            }
            catch
            {
                if (MessageBox.Show("Encountered an error while trying to use the API Key. Would you like to continue without an API Key?", "API Key Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Program.SteamApiKey = "";
                    Invoke(new MethodInvoker(() => Close()));
                }
            }
        }
    }
}