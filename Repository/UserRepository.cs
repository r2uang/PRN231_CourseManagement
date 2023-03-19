using BussinessObject.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUerRepository
    {
        public User getUser(User user) => UserDAO.getUser(user);

    }
}
