using System;
using System.Windows.Forms;

namespace SteamFriendSearchApp
{
    internal static class Program
    {
        /// <summary>
        /// Gets or sets the steam API key.
        /// </summary>
        /// <value>
        /// The steam API key.
        /// </value>
        public static string SteamApiKey { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SteamApiForm());
            if (SteamApiKey != null)
            {
                Application.Run(new FriendSearchForm());
            }
        }
    }
}