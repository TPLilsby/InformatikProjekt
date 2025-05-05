using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lageret.Shared.Models
{
    [Table("Product")]
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int StockLimit { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}

