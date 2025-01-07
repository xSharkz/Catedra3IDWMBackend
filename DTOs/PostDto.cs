using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catedra3IDWMBackend.DTOs
{
    public class PostDto
    {
        public string Title { get; set; } = string.Empty;
        public IFormFile Image { get; set; } = null!;
    }
}