//-----------------------------------------------------------------------
// <copyright file="ForgetPassModel.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace FundooAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Forget password model is class for forget mail sending to the user
    /// </summary>
    public class ForgetPassModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
