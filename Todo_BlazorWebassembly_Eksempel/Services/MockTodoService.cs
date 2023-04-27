﻿using Microsoft.AspNetCore.JsonPatch;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Todo_BlazorWebassembly_Eksempel.Models;

namespace Todo_BlazorWebassembly_Eksempel.Services
{
    public class MockTodoService : ITodoService
    {

        private static int TodoCounter = 1;

        private List<TodoItem> _TodoItems = new List<TodoItem>();

        public MockTodoService()
        {
            _TodoItems = new List<TodoItem>
            {
                new TodoItem()
                {
                    Id = 1,
                    Title = "Lav mad",
                    Description = "Spagetti med kødsovs",
                    TimeCreated = DateTime.Now.AddDays(-3)
                },
                new TodoItem()
                {
                    Id = 2,
                    Title = "Gå tur med hunden",
                    Description = "Gå tur kl 10",
                    TimeCreated = DateTime.Now.AddDays(-10)
                },
                new TodoItem()
                {
                    Id = 3,
                    Title = "Lav et projekt",
                    Description = "lav et vilds kompliceret projekt uden dokumentation så du ikke ved havd du har lavet",
                    TimeCreated = DateTime.Now.AddDays(-30)
                }
            };

            TodoCounter = 4;
        }

        public Task<TodoItem> CreateTodoItemAsync(TodoItem item)
        {
            item.Id = TodoCounter;

            _TodoItems.Add(item);

            return Task.FromResult(item);
        }

        public Task<bool> DeleteTodoItemAsync(int id)
        {
            var selected = _TodoItems.Where(t => t.Id == id).FirstOrDefault();

            if (selected != null)
            {
                _TodoItems.Remove(selected);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<TodoItem> EditTodoItemAsync(TodoItem todoItem)
        {
            var selected = _TodoItems.Where(t => t.Id == todoItem.Id).FirstOrDefault();

            if (selected != null)
            {
                selected.Title = todoItem.Title;
                selected.Description = todoItem.Description;
                return Task.FromResult(selected);
            }
            throw new KeyNotFoundException("Item does not exist");
        }

        public Task<TodoItem> GetTodoItemAsync(int id) => Task.FromResult(_TodoItems.Where(t => t.Id == id).FirstOrDefault() ?? throw new KeyNotFoundException("Item does not exist"));

        public Task<List<TodoItem>> GetTodoItemsAsync() => Task.FromResult(_TodoItems.ToList());

        public Task<TodoItem?> PartiallyEditTodoItemAsync(int TodoId, JsonPatchDocument<TodoItemObject> jsonPatch)
        {
            throw new NotImplementedException();
        }
    }
}
