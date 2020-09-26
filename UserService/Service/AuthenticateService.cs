using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain;

namespace UserService.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        private List<Login> _login = new List<Login>
        {
            new Login { id = 1, firstname = "Ajaya", lastname = "Nahuel", username = "CNA", password = "123" },
            new Login { id = 2, firstname = "AAAAA", lastname = "Aaaaa", username = "AAA", password = "123" }
        };

        public AuthenticateService(IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
        }

        public Domain.SecurityToken Authenticate(string username, string password)
        {
            var user = _login.SingleOrDefault(x => x.username == username && x.password == password);

            if (user == null)
                return null;
            // authentication successful, generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("username", username),
                    new Claim("firstname", user.firstname),
                    new Claim("lastname", user.lastname)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1), // Se configura para que expire dentro de 1 minuto, sacar luego
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtSecurityToken = tokenHandler.WriteToken(token);

            return new Domain.SecurityToken() { auth_token = jwtSecurityToken,expire = tokenDescriptor.Expires };
        }

        public IEnumerable<Login> GetAll()
        {
            return _login;

        }
    }
}
