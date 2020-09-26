using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Domain;

namespace UserService.Service
{
    public interface IAuthenticateService
    {
        SecurityToken Authenticate(string username, string password);

        IEnumerable<Login> GetAll();

    }
}
