using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooAPI.Services
{
    public interface IPushNotification
    {
        Task<bool> SendPushNotification(string deviceTokens, string title, string body, object data);
    }
}
