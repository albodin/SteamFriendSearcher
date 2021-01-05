using System;
using System.Windows.Forms;

namespace SteamFriendSearchApp
{
    /// <summary>
    /// Form to add a game ID to recently played games list
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class AddRecentGameForm : Form
    {
        private readonly FriendSearchForm parentForm;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRecentGameForm"/> class.
        /// </summary>
        /// <param name="_parentForm">The parent form.</param>
        public AddRecentGameForm(Form _parentForm)
        {
            parentForm = _parentForm as FriendSearchForm;
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the addGameIdButton control adding the value
        /// from <see cref="appIdNumericUpDown"/> to the parent forms list of IDs.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addGameIdButton_Click(object sender, EventArgs e)
        {
            parentForm.addRecentGameId(appIdNumericUpDown.Value.ToString());
        }
    }
}