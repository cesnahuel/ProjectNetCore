using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogData.Model
{
    public class BaseEntity
    {
        public string CreateUser { get; set; }
        public string ModifiedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
