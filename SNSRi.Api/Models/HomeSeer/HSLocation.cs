using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNSRi.Api.Models.HomeSeer
{
    public class HSLocation
    {
        public List<string> Floors { get; set; } = new List<string>();
        public List<string> Rooms { get; set; } = new List<string>();
    }
}