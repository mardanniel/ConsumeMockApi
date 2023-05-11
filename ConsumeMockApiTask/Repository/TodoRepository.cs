using ConsumeMockApiTask.Models;
using Newtonsoft.Json;
using System.Text;

namespace ConsumeMockApiTask.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly HttpClient _httpClient;

        public TodoRepository()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        }

        public async Task<Todo> AddTodo(Todo newTodo)
        {
            var newTodoAsString = JsonConvert.SerializeObject(newTodo);
            var responseBody = new StringContent(newTodoAsString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/todos", responseBody);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var todo = JsonConvert.DeserializeObject<Todo>(content);
                return todo;
            }

            return null;
        }

        public async Task DeleteTodo(int todoId)
        {
            var response = await _httpClient.DeleteAsync($"/todos/{todoId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsByteArrayAsync();
                Console.WriteLine("Delete Todo Response: ", data);
            }
        }

        public async Task<List<Todo>> GetAllTodos()
        {
            var response = await _httpClient.GetAsync("/todos");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                List<Todo> todos = JsonConvert.DeserializeObject<List<Todo>>(data);
                return todos;
            }

            return null;
        }

        public async Task<Todo> GetTodoById(int todoId)
        {
            var response = await _httpClient.GetAsync($"/todos/{todoId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var todo = JsonConvert.DeserializeObject<Todo>(content);
                return todo;
            }

            return null;
        }

        public async Task<Todo> UpdateTodo(int todoId, Todo newTodo)
        {
            var newTodoAsString = JsonConvert.SerializeObject(newTodo);
            var responseBody = new StringContent(newTodoAsString, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/todos/{todoId}", responseBody);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var todo = JsonConvert.DeserializeObject<Todo>(content);
                return todo;
            }

            return null;
        }
    }
}
