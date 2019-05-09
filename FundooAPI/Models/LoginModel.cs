//-----------------------------------------------------------------------
// <copyright file="LoginModel.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace FundooAPI.Models
{
    /// <summary>
    /// login model class which have required data to login
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
    }
}
