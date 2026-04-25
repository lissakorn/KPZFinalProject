using Microsoft.EntityFrameworkCore;
using PersonalOrganizer.Data;
using PersonalOrganizer.Models;

namespace PersonalOrganizer.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly OrganizerDbContext _context;

        public CategoryRepository(OrganizerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasTasksAsync(int categoryId)
        {
            return await _context.Tasks.AnyAsync(t => t.CategoryId == categoryId);
        }
    }
}