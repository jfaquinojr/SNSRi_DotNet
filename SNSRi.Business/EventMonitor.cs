using SNSRi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNSRi.Business
{
    public class EventMonitor : IEventMonitor
    {
        private IUnitOfWork _uof;
        public EventMonitor(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public EventServerity CheckEvent(HSEventMessage eventMessage)
        {
            //TODO jfa: DO SOME REAL BUSINESS STUFF HERE
            if (eventMessage is HSEventValueChanged eventValueChanged)
            {
                var device = _uof.Devices.Find(d => d.ReferenceId == eventValueChanged.ReferenceId).FirstOrDefault();
                if(device != null)
                {
                    if (device.Name.ToLower().Contains("emergency"))
                    {
                        return EventServerity.Emergency;
                    }

                    if (device.Name.ToLower().Contains("warning"))
                    {
                        return EventServerity.Warning;
                    }

                    if (device.Name.ToLower().Contains("alert"))
                    {
                        return EventServerity.Alert;
                    }
                }
            }
            
            return EventServerity.Normal;
        }
    }
}
