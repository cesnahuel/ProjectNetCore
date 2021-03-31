using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Domain
{
    public class CategoryProductDTO
    {
        [Required]
        public int IdCategory { get; set; }
    }
}
