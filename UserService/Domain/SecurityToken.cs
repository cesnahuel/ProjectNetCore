using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Domain
{
    public class SecurityToken
    {
        public string auth_token { get; set; }
        public DateTime? expire { get; set; }
    }
}
