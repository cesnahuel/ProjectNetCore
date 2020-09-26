using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusinessData.Model
{
    public class Invoice
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Clave Externa
        [ForeignKey("Client")]
        [Column(Order = 1)]
        public int IdClient { get; set; }

        // Objeto que representa la clave externa.
        [ForeignKey("IdClient")]
        public virtual Client Client { get; set; }

        [Required]
        [Column(Order = 2, TypeName = "nvarchar(15)")]
        public string Number { get; set; }

        [Column(Order = 3, TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Column(Order = 5)]
        public DateTime Date { get; set; }

        public IEnumerable<InvoiceDetail> OrderDetails { get; set; }


    }
}
