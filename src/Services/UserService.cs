using Models.Entities;
using OllsMart;
using System;

namespace ollsmart.Services
{
    public class UserService : IUserService
    {
        private OllsMartContext _dbContext;

        public UserService(OllsMartContext ollsMartContext)
        {
            _dbContext = ollsMartContext;
        }

        public User SaveUser(User user)
        {
            if (user != null)
            {
                if (user.UserId == 0)
                {
                    user.Timestamp = DateTime.UtcNow;
                    user.CreatedTime = DateTime.UtcNow;
                    _dbContext.Users.Add(user);
                }
                else
                {
                    user.Timestamp = DateTime.UtcNow;
                    _dbContext.Users.Add(user);
                }
                _dbContext.SaveChanges();
                return user;
            }
            else
            {
                throw new ArgumentNullException("User");
            }
            
        }
    }
}
