using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Domain
{
    public class Login
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
    }
}
