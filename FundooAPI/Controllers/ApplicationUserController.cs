//-----------------------------------------------------------------------
// <copyright file="ApplicationUserController.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooAPI.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Experimental.System.Messaging;
    using FundooAPI.DataContext;
    using FundooAPI.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// application controller class routes the control
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        /// <summary>
        /// user manager is a class which keep track of the security of the data
        /// </summary>
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// sign manager checks whether the data is present in DB or not
        /// </summary>
        private readonly SignInManager<ApplicationUser> signInManager;

        /// <summary>
        /// application setting is the element in app settings json which is injected in controller
        /// </summary>
        private readonly ApplicationSettings appSettings;

        /// <summary>
        /// email sender is an interface which will send the email to the specific user
        /// </summary>
        private readonly IEmailSender emailSender;

        /// <summary>
        /// I distributed is an interface used for adding feature of redis cache
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// constructor the controller class
        /// </summary>
        /// <param name="userManager">user manager</param>
        /// <param name="signInManager">sign in manager</param>
        /// <param name="appSettings">app settings</param>
        /// <param name="emailSender">email sender</param>
        /// <param name="distributedCache">distributed Cache</param>
        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings, IEmailSender emailSender, IDistributedCache distributedCache)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appSettings = appSettings.Value;
            this.emailSender = emailSender;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// this the method to register the user
        /// </summary>
        /// <param name="model"> application user model </param>
        /// <returns>task interface of action result</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> PostApplicationUser(ApplicationUserModel model)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FullName
            };

            try
            {
                var result = await this.userManager.CreateAsync(applicationUser, model.Password);
                var code = await this.userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                var callbackUrl = Url.Action("ConfirmEmail", "ApplicationUser", new
                {
                    userId = applicationUser.Id,
                    code = code
                },
                protocol: HttpContext.Request.Scheme);
                await this.emailSender.SendEmailAsync(model.Email, "ConfirmAccount", $"Please confirm your account by" + $" clicking this link:<a href='{callbackUrl}'>Link</a>");
                await this.signInManager.SignInAsync(applicationUser, isPersistent: false);
                return this.Ok(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// login method will first check either the data is valid or not and then permit user to login
        /// </summary>
        /// <param name="loginModel">login model</param>
        /// <returns>task of i action result</returns>
        [HttpPost]
        [Route("login")]
        ////POST : /api/ApplicationUser/Login
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await this.userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && await this.userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim("Email",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var jwttoken = new JwtSecurityToken();
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);

                ////adding token to redis cache
                var cacheKey = loginModel.UserName;
                var token = this.distributedCache.GetString(cacheKey);
                if (!string.IsNullOrEmpty(token))
                {
                    return this.Ok(new { token, user });
                }

                token = tokenHandler.WriteToken(securityToken);
                this.distributedCache.SetString(cacheKey, token);
                return this.Ok(new { token, user });
            }
            else
            {
                return this.BadRequest(new { message = "Username or password is incorrect." });
            }
        }

        /// <summary>
        /// log out is method to log out
        /// </summary>
        /// <returns>task of i action result</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            this.distributedCache.Remove("Token");
            return this.Ok();
        }

        /// <summary>
        /// reset password will send mail to the registered email to reset the password
        /// </summary>
        /// <param name="resetPasswordViewModels">reset password model</param>
        [HttpPost]
        [Route("resetpassword")]
        public void SendPasswordResetEmail(ResetPasswordViewModels resetPasswordViewModels)
        {
            // initializing message queue
            Message message = new Message();
            MessageQueue msMq = null;
            const string queueName= @".\private$\TestQueue";
            try
            {
                // MailMessage class is present is System.Net.Mail namespace
                MailMessage mailMessage = new MailMessage("luckykaran95@gmail.com", resetPasswordViewModels.Email);
                Guid guid = Guid.NewGuid();

                // StringBuilder class is present in System.Text namespace
                StringBuilder sbEmailBody = new StringBuilder();
                sbEmailBody.Append("Dear " + resetPasswordViewModels.UserName + ",<br/><br/>");
                sbEmailBody.Append("Please click on the following link to reset your password");
                sbEmailBody.Append("<br/>");
                sbEmailBody.Append(this.appSettings.ForgetLink + guid.ToString());
                sbEmailBody.Append("<br/><br/>");
                sbEmailBody.Append("<b>BridgeIt</b>");

                mailMessage.IsBodyHtml = true;

                mailMessage.Body = sbEmailBody.ToString();
                mailMessage.Subject = "Reset Your Password";
                message.Body = mailMessage;
                message.Recoverable = true;
                message.Formatter = new BinaryMessageFormatter();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            
                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "luckykaran95@gmail.com",
                    Password = "Karansingh22996"
                };

                smtpClient.EnableSsl = true;
                

                // creating directory in msmq
                if (!MessageQueue.Exists(queueName))

                {
                    msMq = MessageQueue.Create(queueName);
                }
                else
                {
                    msMq = new MessageQueue(queueName);
                }
                // sending smtp client to the message queue
                try
                {
                    msMq.Send(message);
                }
                finally {
                    smtpClient.Send(mailMessage);
                    //closing the message queue services
                    msMq.Close();
                }
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// forget method will request for reset link
        /// </summary>
        /// <param name="forgetPassModel">forget password model</param>
        /// <returns>task of i action result</returns>
        [HttpPost]
        [Route("forgetPassword")]
        public async Task<IActionResult> ForgotPassword(ForgetPassModel forgetPassModel)
        {
            var user = await this.userManager.FindByEmailAsync(forgetPassModel.Email);
            if ((user == null) || (await this.userManager.IsEmailConfirmedAsync(user)))
            {
                return null;
            }

            ////send email confirmation
            var code = await this.userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action(
                "SendPasswordResetEmail",
                "ApplicationUser",
                new
                {
                    userId = user.Id,
                    code = code
                },
            protocol: HttpContext.Request.Scheme);
            await this.emailSender.SendEmailAsync(forgetPassModel.Email, "Reset Password", $"Please reset your password by" + $" clicking this link:<a href='{callbackUrl}'>Link</a>");
            return this.Ok();
        }
        [HttpPost]
        [Route("fblogin")]
        public async Task<IActionResult> FbLogin(string email)
        {
            try
            {
                var user = await this.userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                  {
                      new Claim("UserID", user.Id.ToString())
                  }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var cacheKey = email;
                    var token = this.distributedCache.GetString(cacheKey);
                    token = tokenHandler.WriteToken(securityToken);
                    this.distributedCache.SetString(cacheKey, token);
                    return this.Ok(new { token, user });
                }
                else
                {
                    return this.BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
