using PersonalOrganizer.Models;

namespace PersonalOrganizer.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> HasTasksAsync(int categoryId);
    }
}