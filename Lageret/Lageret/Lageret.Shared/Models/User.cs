using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lageret.Shared.Models
{
    [Table("User")]
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }


        public ICollection<UserRole> UserRoles { get; set; }
    }
}