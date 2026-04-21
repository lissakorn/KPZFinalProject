using Microsoft.AspNetCore.Mvc;
using PersonalOrganizer.Models;
using PersonalOrganizer.Repositories;
using System.Threading.Tasks;

namespace PersonalOrganizer.Controllers
{
    public class TaskController : Controller
    {

        private readonly ITaskRepository _repository;

        public TaskController(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {

            var tasks = await _repository.GetAllTasksAsync();

            return View(tasks);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskItem task)
        {
           
            if (ModelState.IsValid)
            {
        
                await _repository.AddTaskAsync(task);

              
                return RedirectToAction("Index");
            }
            return View(task);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var task = await _repository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
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
            return View(task); 
        }
    }
}