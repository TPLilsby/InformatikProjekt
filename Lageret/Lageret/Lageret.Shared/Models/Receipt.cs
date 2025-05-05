using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lageret.Shared.Models
{
    [Table("Receipt")]
    public class Receipt
    {
        public int ReceiptId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
