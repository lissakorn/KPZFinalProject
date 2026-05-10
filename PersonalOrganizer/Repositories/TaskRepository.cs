using Microsoft.EntityFrameworkCore;
using PersonalOrganizer.Data;
using PersonalOrganizer.Models;

namespace PersonalOrganizer.Repositories
{
    public class TaskRepository : Repository<TaskItem>, ITaskRepository
    {
        public TaskRepository(OrganizerDbContext context) : base(context) { }

        public async Task<IEnumerable<TaskItem>> GetFilteredTasksAsync(string searchString, int? categoryId)
        {
            var query = _dbSet.Include(t => t.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                query = query.Where(t => t.Title.Contains(searchString));

            if (categoryId.HasValue)
                query = query.Where(t => t.CategoryId == categoryId.Value);

            return await query.ToListAsync();
        }
    }
}