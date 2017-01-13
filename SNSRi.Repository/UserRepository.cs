using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using SNSRi.Entities;

namespace SNSRi.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SNSRiContext context) : base(context)
        {
        }

        public User ValidateUser(string email, string password)
        {
            return this.SNSRiContext.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public SNSRiContext SNSRiContext => base.Context as SNSRiContext;
    }
}