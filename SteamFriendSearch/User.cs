using System;
using System.Collections.Generic;
using System.Drawing;

namespace SteamFriendSearch
{
    /// <summary>
    /// Class that stores basic info on a Steam user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="username">The users username</param>
        /// <param name="id">Users URL ID</param>
        /// <param name="steamId">The users 64bit Steam ID</param>
        /// <param name="customId">If set to <c>true</c> user has a custom URL ID.</param>
        /// <param name="profilePic">Users profile picture.</param>
        /// <param name="validProfilePicUrl">Whether the users profile picture URL is valid</param>
        /// <param name="otherNames">Other names the user goes by.</param>
        public User(string username, string id, ulong steamId, bool customId, Image profilePic, bool validProfilePicUrl, List<string> otherNames)
        {
            Username = username;
            Id = id;
            CustomId = customId;
            SteamId = steamId;
            ProfilePic = profilePic;
            ValidProfilePicUrl = validProfilePicUrl;
            OtherNames = otherNames;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user has a vanity URL.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the user has a vanity URL; otherwise, <c>false</c>.
        /// </value>
        public bool CustomId { get; set; }

        /// <summary>
        /// Gets or sets the URL ID. If <see cref="CustomId"/> is <c>true</c>
        /// this is the vanity name; otherwise this will equal <see cref="SteamId"/>.
        /// </summary>
        /// <value>
        /// The URL ID.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the steam ID. If <see cref="CustomId"/> is <c>true</c> this
        /// equals 0 as it'd require another request; otherwise this will equal <see cref="Id"/>.
        /// </summary>
        /// <value>
        /// The steam identifier.
        /// </value>
        public ulong SteamId { get; set; }

        /// <summary>
        /// Gets or sets the other names the user went by.
        /// </summary>
        /// <value>
        /// The other names the user went by.
        /// </value>
        public List<string> OtherNames { get; set; }

        /// <summary>
        /// Gets or sets the users profile picture.
        /// </summary>
        /// <value>
        /// The users profile picture.
        /// </value>
        public Image ProfilePic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user has a valid profile picture URL.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the profile picture URL is valid; otherwise, <c>false</c>.
        /// </value>
        public bool ValidProfilePicUrl { get; set; }

        /// <summary>
        /// Gets or sets the users username.
        /// </summary>
        /// <value>
        /// The users username.
        /// </value>
        public string Username { get; set; }
    }
}