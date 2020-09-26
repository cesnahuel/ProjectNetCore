using BusinessData.Model;
using BusinessData.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessData.Repository.Concrete
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository

    {
        private readonly BusinessContext _db;

        public ClientRepository(BusinessContext db) : base(db)
        {
            _db = db;
        }

    }
}
