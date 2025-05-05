using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lageret.Shared.Models
{
    [Table("ReceiptProduct")]
    public class ReceiptProduct
    {
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }
        public Receipt Receipt { get; set; }
        public Product Product { get; set; }
    }
}