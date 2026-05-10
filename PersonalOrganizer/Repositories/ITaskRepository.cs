using PersonalOrganizer.Models;

namespace PersonalOrganizer.Repositories
{
    public interface ITaskRepository : IRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetFilteredTasksAsync(string searchString, int? categoryId);
    }
}