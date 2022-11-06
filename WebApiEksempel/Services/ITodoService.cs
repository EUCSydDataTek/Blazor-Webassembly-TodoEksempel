using WebApiEksempel.Services.Dto;

namespace WebApiEksempel.Services
{
    public interface ITodoService
    {

        public List<TodoItem> GetTodoItems();

        public TodoItem CreateTodo(TodoItem item);

        public TodoItem UpdateTodo(TodoItem item);

        public bool DeleteTodo(int Id);  

        public TodoItem? GetTodo(int id);
    }
}
