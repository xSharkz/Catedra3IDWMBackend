using Microsoft.AspNetCore.Mvc;
using Catedra3IDWMBackend.Models;
using Catedra3IDWMBackend.Services;
using Catedra3IDWMBackend.DTOs;
using System.Threading.Tasks;
using System.Linq;
using Catedra3IDWMBackend.Interfaces;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Catedra3IDWMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly UserManager<User> _userManager;

        public PostController(IPostRepository postRepository, ICloudinaryService cloudinaryService, UserManager<User> userManager)
        {
            _postRepository = postRepository;
            _cloudinaryService = cloudinaryService;
            _userManager = userManager;
        }
        
        [Authorize(Roles = "Usuario")]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] PostDto postDto)
        {
            if (postDto.Image == null || postDto.Image.Length == 0)
                return BadRequest("No se ha subido la imagen.");
            
            if (!postDto.Image.ContentType.StartsWith("image/"))
                return BadRequest("El archivo subido no es una imagen.");
            var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
            var extension = Path.GetExtension(postDto.Image.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                return BadRequest("Solo se permiten imágenes en formato PNG o JPG.");
            if (postDto.Image.Length > 5 * 1024 * 1024)
                return BadRequest("El tamaño de la imagen no puede exceder los 5 MB.");

            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Unauthorized("Usuario no autenticado correctamente.");
            }
            string imageUrl;
            try
            {
                imageUrl = await _cloudinaryService.UploadImageAsync(postDto.Image);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al subir la imagen: {ex.Message}");
            }
            

            var post = new Post
            {
                Title = postDto.Title,
                Date = DateTime.Now,
                Url = imageUrl,
                UserId = user.Id.ToString(),
            };
            
            var postResponse = new PostResponseDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Date = post.Date,
                    Url = post.Url,
                    UserName = user.UserName,
            };

            try
            {
                await _postRepository.CreatePostAsync(post);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear la publicación: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetPosts), new { id = post.Id }, postResponse);
        }

        [Authorize(Roles = "Usuario")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0){
                return BadRequest("Los parámetros 'page' y 'pageSize' deben ser mayores a 0.");
            }
                
            try
            {
                var posts = await _postRepository.GetPagedPostsAsync(page, pageSize);

                var postDtos = posts.Select(post => new PostResponseDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Date = post.Date,
                    Url = post.Url,
                    UserName = post.User?.UserName
                });

                return Ok(postDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener publicaciones: {ex.Message}");
            }
        }
    }

}
