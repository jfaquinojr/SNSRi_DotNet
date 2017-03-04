using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Business
{
    public class EventMonitor
    {
        public EventServerity CheckEvent(HSEventMessage eventMessage)
        {
            //TODO jfa: DO SOME BUSINESS STUFF HERE
            return EventServerity.Normal;
        }
    }
}
