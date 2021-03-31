using System;
using System.Collections.Generic;

#nullable disable

namespace UserData.Models
{
    public partial class TokenUser
    {
        public int IdToken { get; set; }
        public int IdUser { get; set; }
        public string Token { get; set; }
        public DateTime? InitDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual User IdUserNavigation { get; set; }
    }
}
