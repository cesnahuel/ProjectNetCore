using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Domain
{
    public class ProductDTO
    {
        public int idProduct { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitStock { get; set; }
        public int CategoryId { get; set; }
    }
}
