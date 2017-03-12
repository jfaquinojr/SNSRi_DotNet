namespace SNSRi.Business
{
    public interface IEventMonitor
    {
        EventServerity CheckEvent(HSEventMessage eventMessage);
    }
}