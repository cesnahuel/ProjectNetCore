using System;
using System.Collections.Generic;

#nullable disable

namespace UserData.Models
{
    public partial class User
    {
        public User()
        {
            TokenUsers = new HashSet<TokenUser>();
        }

        public int IdUser { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<TokenUser> TokenUsers { get; set; }
    }
}
