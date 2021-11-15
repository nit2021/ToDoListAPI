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
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoService _todoItemService;
        public ToDoListController(IToDoService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        /// <summary>
        /// Method to get List of All TodoList Items
        /// </summary>
        /// <param></param>
        /// <returns>List of TodoList Items</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Method to get List of All TodoList Items")]
        public async Task<ActionResult<IEnumerable<ToDoList>>> GetTodoListItem([FromQuery] OwnerParameters options)
        {
            var todoListItems = await _todoItemService.GetAllTodoList(options);
            if (todoListItems == null)
            {
                return NotFound(new { message = "TodoList Item does not exists" });
            }
            return Ok(todoListItems);

        }

        /// <summary>
        /// Method to get TodoList Item based on given ID
        /// </summary>
        /// <param name="id">Id of TodoList Item</param>
        /// <returns>TodoList Item</returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get TodoList Item based on given ID")]
        public async Task<ActionResult<ToDoList>> GetTodoListItem(long id)
        {
            var todoListItem = await _todoItemService.GetTodoListById(id);

            if (todoListItem == null)
            {
                return NotFound(new { message = "TodoList Item does not exists" });
            }
            return Ok(todoListItem);
        }

        /// <summary>
        /// Method to search TodoList Item based on Search Filter
        /// </summary>
        /// <param name="filter">ToDoList Item description</param>
        /// <returns>TodoList Item</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ToDoList), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Search TodoList Item based on Search Filter")]
        [Route("SearchToDoListItem")]
        public async Task<ActionResult<ToDoList>> SearchTodoListItem([FromQuery] string filter, [FromQuery] OwnerParameters op)
        {
            var result = await _todoItemService.SearchTodoList(filter, op);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }


        /// <summary>
        /// Method to Update TodoList Item based on given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoListDescription"></param>
        /// <returns></returns>
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Update TodoList Item based on given ID")]
        public async Task<IActionResult> PutTodoListItem(long id, [FromQuery] string ItemDesc)
        {
            if (ItemDesc == null || id == 0)
            {
                return BadRequest();
            }

            var result = await _todoItemService.UpdateTodoList(id, ItemDesc);
            if (result == null) return NotFound(); else return NoContent();
        }

        // <summary>
        /// Method to Patch TodoList Item based on given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoListItem"></param>
        /// <returns></returns>
        [HttpPatch("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Method to Patch TodoList Item based on given ID")]
        public async Task<IActionResult> JsonPatchTodoListItem(long id, [FromBody] JsonPatchDocument<ToDoList> todoListItem)
        {
            var item = await _todoItemService.PatchTodoList(id, todoListItem);
            if (item == null)
                return BadRequest();
            else
                return Ok();
        }
        /// <summary>
        /// Method to create a TodoList Item
        /// </summary>
        /// <param name="ItemDesc"></param>
        /// <returns>TodoListItem</returns>
        [HttpPost("PostTodoListItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Creates a TodoList Item")]
        public async Task<ActionResult<ToDoList>> PostTodoListItem([FromQuery] string ItemDesc)
        {
            if (ItemDesc == null)
                return BadRequest(new { message = "TodoList Item Description mandatory" });

            ToDoList item = await _todoItemService.CreateToDoList(ItemDesc);
            return Ok(item);
        }

        /// <summary>
        /// Method to delete TodoList Item of given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Delete TodoList Item of given ID")]
        public async Task<IActionResult> DeleteTodoListItem(long id)
        {
            await _todoItemService.DeleteTodoList(id);
            return NoContent();
        }
    }
}
