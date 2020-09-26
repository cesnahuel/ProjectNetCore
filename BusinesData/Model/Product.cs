using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusinessData.Model
{
    public class Product : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProduct { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitStock { get; set; }
        [ForeignKey("Category"), Column(Order = 0)]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual IEnumerable<InvoiceDetail> InvoiceDetail { get; set; }

    }
}
