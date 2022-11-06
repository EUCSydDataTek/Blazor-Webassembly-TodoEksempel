using WebApiEksempel.Services.Dto;

namespace WebApiEksempel.Services
{
    public class TodoService : ITodoService
    {
        private static int _ItemCount = 1;

        private List<TodoItem> todoItems = new List<TodoItem>();

        public TodoItem CreateTodo(TodoItem item)
        {
            item.Id = _ItemCount;
            _ItemCount++;
            todoItems.Add(item);
            return item;
        }

        public bool DeleteTodo(int Id)
        {
            TodoItem? selectedItem = todoItems.Where(t => t.Id == Id).FirstOrDefault();

            if (selectedItem == null)
            {
                return false;
            }

            todoItems.Remove(selectedItem);
            return true;
        }

        public TodoItem? GetTodo(int id)
        {
            return todoItems.Where(t => t.Id == id).FirstOrDefault();
        }

        public List<TodoItem> GetTodoItems()
        {
            return todoItems.ToList();
        }

        public TodoItem? UpdateTodo(TodoItem item)
        {
            TodoItem? selectedItem = todoItems.Where(t => t.Id == item.Id).FirstOrDefault();

            if (selectedItem != null)
            {
                selectedItem.Title = item.Title;
                selectedItem.Description = item.Description;
            }

            return selectedItem;
        }
    }
}
