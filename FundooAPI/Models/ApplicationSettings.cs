//-----------------------------------------------------------------------
// <copyright file="ApplicationSettings.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace FundooAPI.Models
{
    /// <summary>
    /// application setting is class to access json file data
    /// </summary>
    public class ApplicationSettings
    {
        /// <summary>
        /// Gets or sets the JWT secret.
        /// </summary>
        /// <value>
        /// The JWT secret.
        /// </value>
        public string JWT_Secret { get; set; }

        /// <summary>
        /// Gets or sets the client URL.
        /// </summary>
        /// <value>
        /// The client URL.
        /// </value>
        public string Client_URL { get; set; }

        /// <summary>
        /// Gets or sets the forget link.
        /// </summary>
        /// <value>
        /// The forget link.
        /// </value>
        public string ForgetLink { get; set; }
    }
}
