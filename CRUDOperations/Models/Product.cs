using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDOperations.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string ProductName { get; set; }
        
        public decimal UnitPrice { get; set; }
        [Required]
        public int TotalQuantity { get; set; }
    }
}
