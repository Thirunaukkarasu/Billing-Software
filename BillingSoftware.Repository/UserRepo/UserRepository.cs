using Billing.Domain.Models;
using Billing.Repository.Contracts;
using Billing.Repository.Imp.DBContext;
using System.Linq;

namespace Billing.Repository.Imp.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private POSBillingDBContext dbContext;
        public UserRepository(POSBillingDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public User GetUserDetails(string username)
        {
            return dbContext.User.FirstOrDefault(_ => _.FirstName == username);
        }

        public bool SaveUserDetails(User userDetails)
        {
            dbContext.User.Add(userDetails);
            var rowAffected = dbContext.SaveChanges();
            return rowAffected > 0 ? true : false;
        }
    }
}
