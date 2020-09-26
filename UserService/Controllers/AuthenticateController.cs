using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Domain;
using UserService.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {

        private readonly IAuthenticateService _categoryRepository;

        public AuthenticateController(IAuthenticateService categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("All")]
        public IEnumerable<Login> GetAll()
        {
            var user = _categoryRepository.GetAll();
            return user;
        }

        /// <summary>
        /// Autentica para acceder a los servicios, genera y devuelve un token
        /// </summary>
        /// <param name="login"></param>
        /// <returns>
        /// </returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] Login login)
        {
            if (login == null)
                return BadRequest("Invalid userLogin request");

            var token = _categoryRepository.Authenticate(login.username, login.password);

            if (token == null)
                return BadRequest(new { message = "Username of password incorrect" });

            return Ok(token);
        }

    }
}
