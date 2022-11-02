using Todo_BlazorWebassembly_Eksempel.Models;

namespace Todo_BlazorWebassembly_Eksempel.Services
{
    public interface ITodoService
    {

        public Task<List<TodoItem>> GetTodoItemsAsync();

        public Task<TodoItem> GetTodoItemAsync(int id);

        public Task<TodoItem> EditTodoItemAsync(TodoItem todoItem);

        public Task<TodoItem> DeleteTodoItemAsync(int id);

    }
}
