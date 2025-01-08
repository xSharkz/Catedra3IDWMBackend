using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra3IDWMBackend.Models;
using Microsoft.AspNetCore.Identity;

namespace Catedra3IDWMBackend.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> FindByEmailAsync(string email);
        Task<bool> CreateUserAsync(User user);
    }
}
