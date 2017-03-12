namespace SNSRi.Repository
{
    public interface ITicketingUnitOfWork: IUnitOfWork
    {
        int CreateTicket(string name, string type, int eventId);
    }
}