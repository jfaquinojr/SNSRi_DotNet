using static HomeSeerAPI.Enums;

namespace SNSRi.Plugin
{
    public interface IEventsHub
    {
        void TransmitEvent(string msg);
        void TransmitMessage(string msg);
        void TransmitEvent(HSEvent hsEvent);
    }
}