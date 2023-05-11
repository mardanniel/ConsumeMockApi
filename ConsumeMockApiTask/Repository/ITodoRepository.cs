using ConsumeMockApiTask.Models;

namespace ConsumeMockApiTask.Repository
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllTodos();
        Task<Todo> GetTodoById(int todoId);
        Task<Todo> AddTodo(Todo newTodo);
        Task<Todo> UpdateTodo(int todoId, Todo newTodo);
        Task DeleteTodo(int todoId);
    }
}
