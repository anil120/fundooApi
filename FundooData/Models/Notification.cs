using System;
using System.Collections.Generic;
using System.Text;

namespace FundooData.Models
{
    public class Notification
    {
        public string title { get; set; }
        public string text { get; set; }
        public string[] registration_ids { get; set; }
        public object data { get; set; }
    }
}
