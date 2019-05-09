//-----------------------------------------------------------------------
// <copyright file="ApplicationUser.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace FundooAPI.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// application user is model class which is inheriting identity user class
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUser" />
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string FirstName { get; set; }
        ////public string FullName { get; set; }
        public string ProfilePic { get; set; }
    }
}
