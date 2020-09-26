using BusinessData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessService.Service
{
    public interface IClientService
    {
        IEnumerable<Client> GetAll();
        Client Get(int id);
        void Add(Client client);
    }
}
