using Models.Entities;
using OllsMart;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ollsmart.Services
{
    public class UserService : IUserService
    {
        private OllsMartContext _dbContext;

        public UserService(OllsMartContext ollsMartContext)
        {
            _dbContext = ollsMartContext;
        }

        public List<UserRole>  GetUserRoles()
        {
           return _dbContext.UserRoles.OrderBy(x => x.UserRoleName).ToList();
         
        } 
        public UserRole GetUserRoleById(int id)
        {
            return _dbContext.UserRoles.Where(o => o.UserRoleId==id).FirstOrDefault();        
        }  
        public UserRole SaveUserRole( UserRole userRole)
        {
            if (userRole != null)
            {
                if (userRole.UserRoleId == 0)
                {
                    userRole.Timestamp = DateTime.UtcNow;
                    userRole.CreatedTime = DateTime.UtcNow;
                    _dbContext.UserRoles.Add(userRole);
                }
                else
                {
                    userRole.Timestamp = DateTime.UtcNow;
                    _dbContext.UserRoles.Update(userRole);
                }
                _dbContext.SaveChanges();
                return userRole;
            }
            else
            {
                throw new ArgumentNullException("UserRole");
            }
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
