
namespace SNSRi.Plugin
{
    public interface IEventsHub
    {
        void TransmitMessage(string msg);
        void TransmitEvent(EventMessage eventMessage);
    }
}