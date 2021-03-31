using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserApi.Domain;
using UserApi.Helper;
using UserData.Repository.Interface;
using System.Linq.Expressions;
using UserData.Models;

namespace UserApi.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        private readonly IGenericRepository<User> _genericRepositoryUser;
        private readonly IGenericRepository<TokenUser> _genericRepositoryToken;
        public AuthenticateService(IOptions<AppSettings> appSettings, IGenericRepository<User> genericRepositoryUser, IGenericRepository<TokenUser> genericRepositoryToken)
        {
            this._appSettings = appSettings.Value;
            _genericRepositoryUser = genericRepositoryUser;
            _genericRepositoryToken = genericRepositoryToken;
        }

        public async Task<TokenUser> Authenticate(string username, string password)
        {
            
            string passwordEncrypt = Encrypt.GetSHA256(password);

            Expression<Func<User, bool>> funcPred = p => p.Username == username && p.Password == passwordEncrypt;

            var user = await _genericRepositoryUser.FirstOrDefault(funcPred);

            if (user == null) return null;

            // authentication successful, generate jwt token
            return await GenerateToken(user);
            
        }

        private async Task<TokenUser> GenerateToken(User model) 
        {   
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Username),
                    new Claim(ClaimTypes.Name, string.Format("{0}, {1}",model.FirstName, model.LastName))
                }),
                Expires = DateTime.UtcNow.AddHours(3), // Se configura para que expire dentro de 3 horas, sacar luego
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtSecurityToken = tokenHandler.WriteToken(token);

            TokenUser tokenUser;
            //Registro en base el token para el usuario con su expiracion
            
            tokenUser = new TokenUser()
            {
                IdUser = model.IdUser,
                Token = jwtSecurityToken,
                InitDate = DateTime.Now,
                EndDate = tokenDescriptor.Expires
            };
            await _genericRepositoryToken.AddAsync(tokenUser);

            return tokenUser;
        }


    }
}
