using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Catedra3IDWMBackend.Models
{
    public class Post
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Url(ErrorMessage = "La URL de la imagen debe ser válida.")]
        public string Url { get; set; } = string.Empty;

        [JsonIgnore]
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}
