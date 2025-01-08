using Catedra3IDWMBackend.Models;
using Catedra3IDWMBackend.Interfaces;
using Microsoft.EntityFrameworkCore;
using Catedra3IDWMBackend.Data;

namespace Catedra3IDWMBackend.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts
                .Include(p => p.User)
                .ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(long id)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreatePostAsync(Post post)
        {
            _context.Posts.Add(post);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Post>> GetPagedPostsAsync(int page, int pageSize)
        {
            return await _context.Posts
                .Include(p => p.User) 
                .OrderByDescending(p => p.Date) 
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
