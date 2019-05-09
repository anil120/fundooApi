using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooAPI.Models
{
    public class FireMessage
    {
        public string[] registration_ids { get; set; }
        public Notification notification { get; set; }
        public object data { get; set; }
    }
}
