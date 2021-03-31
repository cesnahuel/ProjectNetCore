using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserApi.Domain;
using UserApi.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {

        private readonly IAuthenticateService _authenticateService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(IAuthenticateService authenticateService, IMapper mapper, ILogger<AuthenticateController> logger)
        {
            _authenticateService = authenticateService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Autentica para acceder a los servicios, genera y devuelve un token
        /// </summary>
        /// <param name="login"></param>
        /// <returns>
        /// </returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate(AuthDto login)
        {
            _logger.LogInformation("Init Authenticate");

            if (login == null)
                return BadRequest(new { message = "Invalid userLogin request" });

            var token = await _authenticateService.Authenticate(login.username, login.password);

            if (token == null)
                return BadRequest(new { message = "Username of password incorrect" });
            
            var result = _mapper.Map<TokenDto>(token);

            _logger.LogInformation("End Authenticate");
            return Ok(result);
        }

    }
}
