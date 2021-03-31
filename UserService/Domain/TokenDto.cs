using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Domain
{
    public class TokenDto
    {
        public string auth_token { get; set; }
        public DateTime? expire { get; set; }
    }
}
