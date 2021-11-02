using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoListAPI.Models;
using ToDoListAPI.Services;

namespace ToDoListAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoService _todoItemService;
        public ToDoListController(IToDoService todoItemService)
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Method to get List of All TodoItems")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetTodoItemList([FromQuery] OwnerParameters options)
        {
            var todoItems = await _todoItemService.GetAllTodoList(options);
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
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Method to get TodoItem based on given ID")]
        public async Task<ActionResult<ToDoItem>> GetTodoItem([FromQuery] long id)
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ToDoItem), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Method to search TodoItem based on Search Filter")]
        [Route("SearchToDoItem")]
        public async Task<ActionResult<ToDoItem>> SearchTodoItem([FromQuery] string filter, [FromQuery] OwnerParameters op)
        {
            var result = await _todoItemService.SearchTodoItem(filter, op);
            return Ok(result);
        }


        /// <summary>
        /// Method to Update TodoItem based on given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Method to Update TodoItem based on given ID")]
        public async Task<IActionResult> PutTodoItem([FromQuery] long id, [FromQuery] string ItemDesc)
        {
            if (ItemDesc == null || id == 0)
            {
                return BadRequest();
            }

            await _todoItemService.UpdateTodoItem(id, ItemDesc);
            return NoContent();
        }

        // <summary>
        /// Method to Patch TodoItem based on given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItem"></param>
        // [{
        // "path": "Description",
        // "op": "replace",
        // "value": "newitempatch2"
        // }]
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Method to Patch TodoItem based on given ID")]
        [Route("PatchToDoItem")]
        public async Task<IActionResult> JsonPatchTodoItem(long id, [FromBody] JsonPatchDocument<ToDoItem> todoItem)
        {
            var item = await _todoItemService.PatchTodoItem(id, todoItem);
            if (item == null)
                return BadRequest();
            else
                return Ok();
        }
        /// <summary>
        /// Method to create a TodoItem
        /// </summary>
        /// <param name="ItemDesc"></param>
        /// <returns>TodoItem</returns>
        [HttpPost("PostTodoItem")]
        //[Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Method to create a TodoItem")]
        public async Task<ActionResult<ToDoItem>> PostTodoItem([FromQuery] string ItemDesc)
        {
            if (ItemDesc == null)
                return BadRequest(new { message = "TodoItem Description mandatory" });

            ToDoItem item = await _todoItemService.CreateTodoItem(ItemDesc);
            return item;
        }

        /// <summary>
        /// Method to create a Label
        /// </summary>
        /// <param name="ItemDesc"></param>
        /// <param name="LabelDesc"></param>
        /// <returns>Label</returns>
        [HttpPost("PostLabel")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Method to create a Label")]
        public async Task<ActionResult<Label>> PostLabel([FromQuery] int ItemId, [FromQuery] string LabelDesc)
        {
            if (LabelDesc == null || ItemId == 0)
                return BadRequest(new { message = "TodoItem Description mandatory" });

            Label item = await _todoItemService.CreateLabel(ItemId, LabelDesc);
            return item;
        }

        /// <summary>
        /// Method to delete TodoItem of given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteTodoItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Method to delete TodoItem of given ID")]
        public async Task<IActionResult> DeleteTodoItem([FromQuery] long id)
        {
            await _todoItemService.DeleteTodoItem(id);
            return NoContent();
        }

        /// <summary>
        /// Method to delete Label of given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteLabel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(Summary = "Method to delete Label of given ID")]
        public async Task<IActionResult> DeleteLabel([FromQuery] long id)
        {
            await _todoItemService.DeleteLabel(id);
            return NoContent();
        }
    }
}
