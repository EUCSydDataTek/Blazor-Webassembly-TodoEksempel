using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiEksempel.Services;
using WebApiEksempel.Services.Dto;

namespace WebApiEksempel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        private readonly ITodoService _TodoService;

        public TodoController(ITodoService todoService)
        {
            _TodoService = todoService;
        }

        /// <summary>
        /// Get Todo Items
        /// </summary>
        /// <returns>Todo Items</returns>
        [HttpGet]
        public List<TodoItem> GetItems() => _TodoService.GetTodoItems();

        /// <summary>
        /// Get Todo Item
        /// </summary>
        /// <param name="Id">The id of the todo item</param>
        /// <returns>Result</returns>
        [HttpGet]
        [Route("item/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(TodoItem))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetItemById(int Id)
        {
            var todo = _TodoService.GetTodo(Id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        /// <summary>
        /// Creates a todo item
        /// </summary>
        /// <param name="todoItem">todo item to be created</param>
        /// <returns>The route the item is created at</returns>
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created,Type = typeof(TodoItem))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateItem(TodoItem todoItem)
        {
            var newitem = _TodoService.CreateTodo(todoItem);

            return Created($"/api/item/{newitem.Id}",newitem);
        }

        /// <summary>
        /// Deletes a item in the todolist
        /// </summary>
        /// <param name="id">The id of the item</param>
        /// <returns>A result of the action</returns>
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteItem(int id)
        {
          if(_TodoService.DeleteTodo(id))
          {
                return Ok("Item deleted");
          }
          else
          {
                return BadRequest();
          }
        }

        /// <summary>
        /// Updates TodoItem
        /// </summary>
        /// <param name="todoItem">The todoItem to be Updated</param>
        /// <returns>Route of the updated item</returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status202Accepted,Type = typeof(TodoItem))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateItem(TodoItem todoItem)
        {
            var updatedItem = _TodoService.UpdateTodo(todoItem);

            if (updatedItem != null)
            {
                return Accepted($"item/{updatedItem.Id}", updatedItem);
            }
            else
            {
                return NotFound();
            }
        }


    }
}
