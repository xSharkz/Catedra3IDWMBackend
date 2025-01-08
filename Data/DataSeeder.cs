using Catedra3IDWMBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Catedra3IDWMBackend.Data
{
    public class DataSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task SeedRolesAsync()
        {
            var roles = new List<string> { "Usuario", "Admin" };

            foreach (var role in roles)
            {
                var existingRole = await _roleManager.FindByNameAsync(role);
                if (existingRole == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
