using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessData.Model;
using BusinessService.Domain;
using BusinessService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessService.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, IMapper mapper, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/<controller>
        [HttpGet("All")]
        public IActionResult Get()
        {
            
            var clients =_clientService.GetAll();
            var viewModel = _mapper.Map<IEnumerable<ClientDTO>>(clients);
            
            return Ok(viewModel);
        }

     
        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] ClientDTO client)
        {
            _logger.LogInformation("Ingresa");
            var _client = _mapper.Map<Client>(client);
            _clientService.Add(_client);
        }

      
    }
}
