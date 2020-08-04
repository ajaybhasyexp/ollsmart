using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ollsmart.Services
{
    public interface IUserService
    {
        
        List<UserRole>  GetUserRoles();
        UserRole GetUserRoleById(int id);
        UserRole SaveUserRole( UserRole userRole);
        User SaveUser(User user);
    }
}
