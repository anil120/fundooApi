using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FundooAPI.Models;
using FundooAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FundooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushNotificationController : ControllerBase
    {
        private readonly IPushNotification _pushNotification;

        public PushNotificationController(IPushNotification pushNotification)
        {
            _pushNotification = pushNotification;
        }
        [HttpPost("{deviceTokens}")]
        public async Task<bool> SendPushNotification([FromRoute]string deviceTokens,[FromBody] Notification notification)   
        {
            string token = deviceTokens;
            return await _pushNotification.SendPushNotification(token,notification.title,notification.text,notification.data);
        }
    }
}