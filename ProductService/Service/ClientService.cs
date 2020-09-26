using BusinessData.Model;
using BusinessData.Repository.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessService.Service
{
    public class ClientService : IClientService
    {
        private readonly AppSettings _appSettings;
        private readonly IClientRepository _clientRepository;


        public ClientService(IOptions<AppSettings> appSettings, IClientRepository clientRepository)
        {
            this._appSettings = appSettings.Value;
            this._clientRepository = clientRepository;
        }

        public Client Get(int id)
        {
            var client = _clientRepository.Get(id);
            return client;
        }

        public IEnumerable<Client> GetAll()
        {
            return _clientRepository.GetAll();
        }

        public void Add(Client client)
        {
            this._clientRepository.Add(client);
        }
        
    }
}
