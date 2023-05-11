using ConsumeMockApiTask.Models;
using ConsumeMockApiTask.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ConsumeMockApiTask.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            this._todoRepository = todoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Todo> todos = await this._todoRepository.GetAllTodos();
            return View(todos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Todo newTodo)
        {
            newTodo.UserId = 1;
            newTodo.Completed = false;

            await this._todoRepository.AddTodo(newTodo);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int todoId)
        {
            var todo = await this._todoRepository.GetTodoById(todoId);

            if (todo is null)
                return NotFound();

            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Todo updatedTodo)
        {
            var test = await this._todoRepository.UpdateTodo(updatedTodo.Id, updatedTodo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int todoId)
        {
            await this._todoRepository.DeleteTodo(todoId);
            return RedirectToAction("Index");
        }
    }
}
