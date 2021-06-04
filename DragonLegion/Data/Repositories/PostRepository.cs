using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonLegion.Models;
using Microsoft.EntityFrameworkCore;

namespace DragonLegion.Data.Repositories
{
    public class PostRepository
    {
        private readonly ApplicationDbContext dbContext;
        
        public PostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Post> GetBy(long id) =>
            await dbContext.Posts
                .Include(p => p.Author)
                .SingleOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Post>> GetAll() =>
            await dbContext.Posts
                .Include(p => p.Author)
                .OrderBy(p => p.CreationDate)
                .ToListAsync();

        public async Task Add(Post post) =>
            await dbContext.Posts.AddAsync(post);

        public void Update(Post post) =>
            dbContext.Posts.Update(post);
        
        public async Task Delete(long id) =>
            dbContext.Posts.Remove(await dbContext.Posts
                    .FirstAsync(p => p.Id == id));

        public async Task SaveChangesAsync() =>
            await dbContext.SaveChangesAsync();
    }
}
