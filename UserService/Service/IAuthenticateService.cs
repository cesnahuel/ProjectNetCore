using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Domain;
using UserData.Models;

namespace UserApi.Service
{
    public interface IAuthenticateService
    {
        Task<TokenUser> Authenticate(string username, string password);
    }
}
