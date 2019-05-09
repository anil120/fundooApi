//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using FundooData.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace FundooAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class NotificationController : ControllerBase
//    {
//        private static Uri FireBasePushNotificationsURL = new Uri("https://fcm.googleapis.com/fcm/send");
//        private static string ServerKey = "AIzaSyDTOKxJWK6av2FNzJZOuQRw1xzUvXBiHUg";

//        [HttpPost]
//        public async Task<bool> GetNotification(string[] deviceTokens, string title, string body, object data)
//        {
//            bool sent = false;

//            if (deviceTokens.Count() > 0)
//            {
//                //Object creation

//                var messageInformation = new Message()
//                {
//                    notification = new Notification()
//                    {
//                        title = title,
//                        text = body
//                    },
//                    data = data,
//                    registration_ids = deviceTokens
//                };
//                string jsonMessage = JsonConvert.SerializeObject(messageInformation);
//                var request = new HttpRequestMessage(HttpMethod.Post, FireBasePushNotificationsURL);

//                request.Headers.TryAddWithoutValidation("Authorization", "key=" + ServerKey);
//                request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

//                HttpResponseMessage result;
//                using (var client = new HttpClient())
//                {
//                    result = await client.SendAsync(request);
//                    sent = sent && result.IsSuccessStatusCode;
//                }
//            }

//            return sent;
//        }
//    }
//}
