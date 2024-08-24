 using Billing.Domain.Models;

namespace Billing.Repository.Contracts
{
    public interface IUserRepository
    {
        User GetUserDetails(string username);
        bool SaveUserDetails(User userDetails);
    }
}
