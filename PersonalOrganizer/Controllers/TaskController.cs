using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalOrganizer.Data;
using PersonalOrganizer.Models;
using PersonalOrganizer.Repositories;
using System.Threading.Tasks;

namespace PersonalOrganizer.Controllers
{
    public class TaskController : Controller
    {

        private readonly ITaskRepository _repository;
        private readonly OrganizerDbContext _context;

        public TaskController(ITaskRepository repository, OrganizerDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, int? categoryId)
        {
            var tasksQuery = _context.Tasks.Include(t => t.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                tasksQuery = tasksQuery.Where(t => t.Title.Contains(searchString));
            }

            if (categoryId.HasValue)
            {
                tasksQuery = tasksQuery.Where(t => t.CategoryId == categoryId.Value);
            }

            await PopulateCategoriesViewBag(categoryId);
            ViewBag.CurrentSearch = searchString; 

            return View(await tasksQuery.ToListAsync());
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
            if (ModelState.IsValid)
            {
                await _repository.AddTaskAsync(task); 
                return RedirectToAction(nameof(Index));
            }

            await PopulateCategoriesViewBag();
            return View(task);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var task = await _repository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            await PopulateCategoriesViewBag(task.CategoryId); 
            return View(task);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateTaskAsync(task); 
                return RedirectToAction("Index"); 
            }
            await PopulateCategoriesViewBag(task.CategoryId);
            return View(task);
        }
      
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _repository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteTaskAsync(id);
            return RedirectToAction("Index");
        }

        private async Task PopulateCategoriesViewBag(int? selectedId = null)
        {
            var categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", selectedId);
        }
    }
}