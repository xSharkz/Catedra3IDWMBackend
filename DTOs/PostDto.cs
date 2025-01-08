using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catedra3IDWMBackend.DTOs
{
    public class PostDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "El título debe tener al menos 5 caracteres.")]
        public string Title { get; set; } = string.Empty;
        public IFormFile Image { get; set; } = null!;
    }
}
