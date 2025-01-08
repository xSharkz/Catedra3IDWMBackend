using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catedra3IDWMBackend.DTOs
{
    public class PostResponseDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
    }
}