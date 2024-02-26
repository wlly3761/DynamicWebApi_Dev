using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User
{
    public interface IUserService
    {
         string GetUserName(string userId);

         string UserName { get; }
    }
}
