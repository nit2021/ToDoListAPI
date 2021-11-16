using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoAPI.Core.Models;
using ToDoListAPI.ToDoAPI.Services;

namespace ToDoListAPI.ToDoAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoService _todoItemService;
        public ToDoItemController(IToDoService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        /// <summary>
        /// Method to get List of All TodoItems
        /// </summary>
        /// <param></param>
        /// <returns>List of TodoItems</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get List of All TodoItems")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetTodoItemList([FromQuery] OwnerParameters options)
        {
            var todoItems = await _todoItemService.GetAllTodoItem(options);
            if (todoItems == null)
            {
                return NotFound(new { message = "TodoItem does not exists" });
            }
            return Ok(todoItems);
        }

        /// <summary>
        /// Method to get TodoItem based on given ID
        /// </summary>
        /// <param name="id">Id of TodoItem</param>
        /// <returns>TodoItem</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get TodoItem based on given ID")]
        public async Task<ActionResult<ToDoItem>> GetTodoItem(int id)
        {
            var todoItem = await _todoItemService.GetTodoItemById(id);

            if (todoItem == null)
            {
                return NotFound(new { message = "Todo Item does not exists" });
            }
            return Ok(todoItem);
        }

        /// <summary>
        /// Method to search TodoItem based on Search Filter
        /// </summary>
        /// <param name="filter">Item description</param>
        /// <returns>TodoItem</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ToDoItem), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Search TodoItem based on Search Filter")]
        [Route("SearchToDoItem")]
        public async Task<ActionResult<ToDoItem>> SearchTodoItem([FromQuery] string filter, [FromQuery] OwnerParameters op)
        {
            var result = await _todoItemService.SearchTodoItem(filter, op);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        /// <summary>
        /// Method to Update TodoItem based on given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Update TodoItem based on given ID")]
        public async Task<IActionResult> PutTodoItem(long id, [FromQuery] string ItemDesc)
        {
            if (ItemDesc == null || id == 0)
                return BadRequest();

            var result = await _todoItemService.UpdateTodoItem(id, ItemDesc);

            if (result == null) return NotFound(); else return NoContent();
        }

        // <summary>
        /// Method to Patch TodoItem based on given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        [HttpPatch("{id:long}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Patch TodoItem based on given ID")]
        public async Task<IActionResult> JsonPatchTodoItem(long id, [FromBody] JsonPatchDocument<ToDoItem> todoItem)
        {
            var item = await _todoItemService.PatchTodoItem(id, todoItem);
            if (item == null)
                return BadRequest();
            else
                return Ok(item);
        }

        /// <summary>
        /// Method to create a TodoItem
        /// </summary>
        /// <param name="ItemDesc"></param>
        /// <returns>TodoItem</returns>
        [HttpPost("PostTodoItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Create a TodoItem")]
        public async Task<ActionResult<ToDoItem>> PostTodoItem([FromQuery] int taskId, [FromQuery] string ItemDesc)
        {
            if (ItemDesc == null)
                return BadRequest(new { message = "TodoItem Description mandatory" });

            ToDoItem item = await _todoItemService.CreateTodoItem(taskId, ItemDesc);
            return Ok(item);
        }

        /// <summary>
        /// Method to delete TodoItem of given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Delete TodoItem for given ID")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _todoItemService.DeleteTodoItem(id);
            return NoContent();
        }

    }
}
