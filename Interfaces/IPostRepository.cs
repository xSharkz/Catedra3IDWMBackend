using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra3IDWMBackend.Models;

namespace Catedra3IDWMBackend.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(long id);
        Task CreatePostAsync(Post post);
        Task SaveChangesAsync();
        Task<IEnumerable<Post>> GetPagedPostsAsync(int page, int pageSize);
    }
}
