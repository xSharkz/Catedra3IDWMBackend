using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Catedra3IDWMBackend.Models;
using Catedra3IDWMBackend.Services;
using Catedra3IDWMBackend.DTOs;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Catedra3IDWMBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager; 
        private readonly JWTService _jwtService;

        public AuthController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, JWTService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {  
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return BadRequest("El correo electrónico ya está en uso.");
            }
            if (registerDto.Password.Length < 6 || !registerDto.Password.Any(char.IsDigit))
            {
                return BadRequest("La contraseña debe tener al menos 6 caracteres y contener al menos un número.");
            }
            var role = await _roleManager.FindByNameAsync("Usuario");
            if (role == null)
            {
                role = new IdentityRole("Usuario");
                await _roleManager.CreateAsync(role);
            }

            var user = new User
            {
                UserName = registerDto.Email,  
                Email = registerDto.Email.ToLower(),
            };

            var createUserResult = await _userManager.CreateAsync(user, registerDto.Password);
            if (!createUserResult.Succeeded)
            {
                var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
                return BadRequest($"Error al crear el usuario: {errors}");
            }

            await _userManager.AddToRoleAsync(user, "Usuario");

            return Ok(new { Message = "Usuario registrado con éxito." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email.ToLower()) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Correo electrónico y contraseña son requeridos.");
            }
            var user = await _userManager.FindByEmailAsync(loginDto.Email.ToLower());
            if (user == null)
            {
                return Unauthorized("Correo electrónico no encontrado.");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid){
                return Unauthorized("Contraseña incorrecta.");
            }
            var userRoles = await _userManager.GetRolesAsync(user); 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email), 
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) 
            };
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }
            var token = _jwtService.GenerateJWTToken(claims);
            return Ok(new { Token = token });
        }
    }
}
