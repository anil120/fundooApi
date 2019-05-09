//-----------------------------------------------------------------------
// <copyright file="ResetPasswordViewModels.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace FundooAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// reset password model class consist of all the data to reset the data from the database
    /// </summary>
    public class ResetPasswordViewModels
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
