using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lageret.Shared.Models
{
    [Table("OrderEntry")]
    public class OrderEntry
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}
