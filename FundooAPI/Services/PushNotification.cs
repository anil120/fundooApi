using FundooAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FundooAPI.Services
{
    public class PushNotification: IPushNotification
    {
        private Uri FireBasePushNotificationsURL = new Uri("https://fcm.googleapis.com/fcm/send");
        private string ServerKey = "AAAAb2HcBc0:APA91bE4Xvd-kSF0p-NjHhriao0zqN6Nl4-ms93s5V15qa1RB0oc-US0O_YnVxqPwIveDbxwQJnvB5kYK54NDSfh5kh3E-04mu3rGzWV9Cjfmm3TeOBuJWh2Wja-FSaEP1e3tlu-GGY1";

        public async Task<bool> SendPushNotification(string deviceTokens, string title, string body, object data)
        {
            bool sent = false;

            if (deviceTokens!=null)
            {
                //Object creation


                var notification = new Notification()
                {

                    title = title,
                    text = body,


                    data = data,
                    registration_ids = deviceTokens
                };

                //Object to JSON STRUCTURE => using Newtonsoft.Json;
                string jsonMessage = JsonConvert.SerializeObject(notification);

                /*
                 ------ JSON STRUCTURE ------
                 {
                    notification: {
                                    title: "",
                                    text: ""
                                    },
                    data: {
                            action: "Play",
                            playerId: 5
                            },
                    registration_ids = ["id1", "id2"]
                 }
                 ------ JSON STRUCTURE ------
                 */

                //Create request to Firebase API
                var request = new HttpRequestMessage(HttpMethod.Post, FireBasePushNotificationsURL);

                request.Headers.TryAddWithoutValidation("Authorization", "key=" + ServerKey);
                request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    result = await client.SendAsync(request);
                    sent = sent && result.IsSuccessStatusCode;
                }
            }

            return sent;
        }
    }
}
