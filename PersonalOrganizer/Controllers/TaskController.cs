using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalOrganizer.Models;
using PersonalOrganizer.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalOrganizer.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public TaskController(ITaskRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index(string searchString, int? categoryId)
        {
            return View(await _repository.GetFilteredTasksAsync(searchString, categoryId));
        }

        public async Task<IActionResult> Create()
        {
            await PopulateCategoriesViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesViewBag();
                return View(task);
            }

            await _repository.AddTaskAsync(task);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return await ProcessTaskAction(id, async task =>
            {
                await PopulateCategoriesViewBag(task.CategoryId);
                return View(task);
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesViewBag(task.CategoryId);
                return View(task);
            }

            await _repository.UpdateTaskAsync(task);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await ProcessTaskAction(id, task => Task.FromResult<IActionResult>(View(task)));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteTaskAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> ProcessTaskAction(int? id, Func<TaskItem, Task<IActionResult>> action)
        {
            if (id == null) return NotFound();

            var task = await _repository.GetTaskByIdAsync(id.Value);
            if (task == null) return NotFound();

            return await action(task);
        }

        private async Task PopulateCategoriesViewBag(int? selectedId = null)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories.OrderBy(c => c.Name), "Id", "Name", selectedId);
        }
    }
}