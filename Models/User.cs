using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catedra3IDWMBackend.Models
{
    public class Usuario
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string ApplicationUserId { get; set; } = string.Empty;
    public ApplicationUser ApplicationUser { get; set; } = null!;

    public ICollection<Post> Posts { get; set; } = null!;
}
}