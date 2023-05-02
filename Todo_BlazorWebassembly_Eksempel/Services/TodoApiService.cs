using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using Todo_BlazorWebassembly_Eksempel.Models;

namespace Todo_BlazorWebassembly_Eksempel.Services
{
    public class TodoApiService : ITodoService
    {
        private readonly HttpClient _HttpClient;

        public TodoApiService(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

        public async Task<TodoItem> CreateTodoItemAsync(TodoItem item)
        {
            TodoItemObject todoItemObject = item.ToTodoItemObject();

            var response = await _HttpClient.PostAsJsonAsync<TodoItemObject>("/api/todo/create",todoItemObject);

            response.EnsureSuccessStatusCode();

            todoItemObject = await response.Content.ReadFromJsonAsync<TodoItemObject>();
            return todoItemObject.ToTodoItem();

        }

        public async Task<bool> DeleteTodoItemAsync(int id)
        {
            var result = await _HttpClient.DeleteAsync($"/api/todo/delete?id={id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<TodoItem> EditTodoItemAsync(TodoItem todoItem)
        {
            TodoItemObject todoItemObject = todoItem.ToTodoItemObject();

            var response = await _HttpClient.PutAsJsonAsync<TodoItemObject>("/api/todo/update", todoItemObject);

            if (response.IsSuccessStatusCode)
            {
                todoItemObject = await response.Content.ReadFromJsonAsync<TodoItemObject>();
                return todoItemObject.ToTodoItem();
            }
            throw new KeyNotFoundException("Item does not exist");
        }

        public async Task<TodoItem?> PartiallyEditTodoItemAsync(int TodoId,JsonPatchDocument<TodoItemObject> jsonPatch)
        {
            string PatchCommands = JsonConvert.SerializeObject(jsonPatch);

            var requestContent = new StringContent(PatchCommands, Encoding.UTF8, "application/json-patch+json");

            var response = await _HttpClient.PatchAsync($"/api/todo/update/{TodoId}",requestContent);

            response.EnsureSuccessStatusCode();

            var Item = await response.Content.ReadFromJsonAsync<TodoItemObject>();

            return Item?.ToTodoItem() ?? null;
        }

        public async Task<TodoItem> GetTodoItemAsync(int id)
        {
            var item = await _HttpClient.GetFromJsonAsync<TodoItemObject>($"/api/todo/item/{id}");

            if (item != null)
            {
                return item.ToTodoItem();
            }
            throw new KeyNotFoundException("Item does not exist");
        }

        public async Task<List<TodoItem>> GetTodoItemsAsync()
        {
            var items = await _HttpClient.GetFromJsonAsync<List<TodoItemObject>>("/api/todo");

            return items
                    .AsQueryable()
                    .MapTodoItems()
                    .ToList();
        }
    }

    public class TodoItemObject
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime dateCreated { get; set; }
        public bool completed { get; set; }
    }

    public static class TodoObjectMapper
    {
        public static TodoItemObject ToTodoItemObject(this TodoItem item)
        {
            return new TodoItemObject
            {
                completed = item.IsCompleted,
                dateCreated = item.TimeCreated,
                description = item.Description,
                title = item.Title,
                id = item.Id
            };
        }

        public static TodoItem ToTodoItem(this TodoItemObject item)
        {
            return new TodoItem
            {
                IsCompleted = item.completed,
                TimeCreated = item.dateCreated,
                Description = item.description,
                Title = item.title,
                Id = item.id
            };
        }

        public static IQueryable<TodoItem> MapTodoItems(this IQueryable<TodoItemObject> items)
        {
            return items.Select(it => new TodoItem
            {
                IsCompleted = it.completed,
                TimeCreated = it.dateCreated,
                Description = it.description,
                Title = it.title,
                Id = it.id
            });
        }
    }

}
