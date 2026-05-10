using Microsoft.EntityFrameworkCore;
using PersonalOrganizer.Data;
using PersonalOrganizer.Models;

namespace PersonalOrganizer.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(OrganizerDbContext context) : base(context) { }

        public async Task<bool> HasTasksAsync(int categoryId)
        {
            return await _context.Tasks.AnyAsync(t => t.CategoryId == categoryId);
        }
    }
}