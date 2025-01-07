using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Catedra3IDWMBackend.Models
{
    public class ApplicationUser : IdentityUser {
        [Key]
        public long ApplicationUserId { get; set; }
        
        [Required]
        [MinLength(5, ErrorMessage = "El rol debe tener al menos 5 caracteres.")]
        public string Name { get; set; } = string.Empty;
    }

}