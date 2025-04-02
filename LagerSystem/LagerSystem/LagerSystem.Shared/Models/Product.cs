using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem.Shared.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Category { get; set; }

        public string? Supplier { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        [Required]
        public decimal Price { get; set; }  

        public int ReorderLevel { get; set; } // Niveau for genbestilling

        public bool NeedsReorder => StockQuantity <= ReorderLevel;

        [NotMapped]
        public int ReceiveAmount { get; set; } // Bruges kun i UI

    }
}
