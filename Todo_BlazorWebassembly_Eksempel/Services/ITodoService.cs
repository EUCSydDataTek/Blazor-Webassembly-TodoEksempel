using Microsoft.AspNetCore.JsonPatch;
using Todo_BlazorWebassembly_Eksempel.Models;

namespace Todo_BlazorWebassembly_Eksempel.Services
{
    public interface ITodoService
    {

        public Task<List<TodoItem>> GetTodoItemsAsync();

        public Task<TodoItem> GetTodoItemAsync(int id);

        public Task<TodoItem> CreateTodoItemAsync(TodoItem item);

        public Task<TodoItem> EditTodoItemAsync(TodoItem todoItem);

        public Task<bool> DeleteTodoItemAsync(int id);

        public Task<TodoItem?> PartiallyEditTodoItemAsync(int TodoId, JsonPatchDocument<TodoItemObject> jsonPatch);

    }
}
