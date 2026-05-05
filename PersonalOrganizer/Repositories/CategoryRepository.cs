using Microsoft.EntityFrameworkCore;
using PersonalOrganizer.Data;
using PersonalOrganizer.Models;
using System;

namespace PersonalOrganizer.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly OrganizerDbContext _context;

        public CategoryRepository(OrganizerDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            return await _context.Categories.FindAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

       public async Task DeleteCategoryAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

       public async Task<bool> HasTasksAsync(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("CategoryId must be greater than zero.", nameof(categoryId));

            return await _context.Tasks.AnyAsync(t => t.CategoryId == categoryId);
        }
    }
}
