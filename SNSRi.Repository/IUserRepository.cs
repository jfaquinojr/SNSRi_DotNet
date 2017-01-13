using SNSRi.Entities;

namespace SNSRi.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User ValidateUser(string email, string password);
    }
}