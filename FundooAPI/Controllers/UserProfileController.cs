//-----------------------------------------------------------------------
// <copyright file="UserProfileController.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace WebAPI.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FundooAPI.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    /// <summary>
    /// user profile class will fetch all the data of login user
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        /// <summary>
        /// user manager is class which will check either the given data are valid and present in the database
        /// </summary>
        private UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// constructor of the controller class
        /// </summary>
        /// <param name="userManager"></param>
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// get user profile method will fetch the data of the logged in user 
        /// </summary>
        /// <returns>object of the user model</returns>
        [HttpGet]
        //GET : /api/UserProfile
        [ValidateAntiForgeryToken]
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.FirstName,
                user.Email,
                user.UserName
            };
        }
    }
}