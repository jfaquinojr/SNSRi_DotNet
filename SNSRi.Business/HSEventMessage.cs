﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Business
{
    public class HSEventMessage
    {
        public int HSEventType { get; set; }
        public List<string> Parameters { get; set; }
    }
}
