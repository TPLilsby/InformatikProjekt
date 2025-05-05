using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lageret.Shared.Models
{
    [Table("Category")]
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
