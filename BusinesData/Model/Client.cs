using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusinessData.Model
{
    public class Client : BaseEntity
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(Order = 1, TypeName = "nvarchar(10)")]
        public string Number { get; set; }

        [Column(Order = 2, TypeName = "nvarchar(50)")]
        public string Surname { get; set; }

        [Column(Order = 3, TypeName = "nvarchar(50)")]
        public string Firstame { get; set; }

        [Column(Order = 4, TypeName = "nvarchar(15)")]
        public string Phone { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

    }
}
