using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerSystem.Shared.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; } // Skal kun gemme hash

        [Required]
        public string Role { get; set; }

        [NotMapped]
        public string Password { get; set; } // Bruges til binding i UI
    }

}
