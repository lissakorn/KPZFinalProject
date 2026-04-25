using PersonalOrganizer.Models;

namespace PersonalOrganizer.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);

       
        Task<bool> HasTasksAsync(int categoryId);
    }
}