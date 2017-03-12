using SNSRi.Entities;

namespace SNSRi.Repository
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(SNSRiContext context) : base(context)
        {
        }

        public SNSRiContext SNSRiContext => base.Context as SNSRiContext;
    }
}