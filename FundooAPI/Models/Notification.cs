using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooAPI.Models
{
    public class Notification
    {
        public string title { get; set; }
        public string text
        {
            get; set;

        }
        public string registration_ids { get; set; }
        public object data { get; set; }
    }
}
