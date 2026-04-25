using Microsoft.AspNetCore.Mvc;
using PersonalOrganizer.Models;
using PersonalOrganizer.Repositories;

namespace PersonalOrganizer.Controllers
{
    public class CategoriesController : Controller
    {
     
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllCategoriesAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var category = await _repository.GetCategoryByIdAsync(id.Value);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool hasTasks = await _repository.HasTasksAsync(id);

            if (hasTasks)
            {
                TempData["ErrorMessage"] = "Цю категорію не можна видалити, бо в ній є завдання! Спочатку перенесіть їх в іншу категорію.";
                return RedirectToAction(nameof(Index));
            }

            var category = await _repository.GetCategoryByIdAsync(id);
            if (category != null)
            {
                await _repository.DeleteCategoryAsync(category);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}