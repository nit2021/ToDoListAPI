using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoListAPI.Models;
using ToDoListAPI.Services;

namespace ToDoListAPI.Controllers
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
        /// Method to get List of All TodoItems
        /// </summary>
        /// <param></param>
        /// <returns>List of TodoItems</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetTodoItems([FromQuery]OwnerParameters options)
        {
           var todoItems= await _todoItemService.GetAllTodoList(options);
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
        public async Task<ActionResult<ToDoItem>> GetTodoItem([FromQuery]long id)
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
        /// <param name="id">Id of TodoItem</param>
        /// <returns>TodoItem</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ToDoItem), (int)HttpStatusCode.OK)]
        [Route("SearchToDoItem")]
        public async Task<ActionResult<ToDoItem>> SearchTodoItem([FromQuery] string filter, [FromQuery]OwnerParameters op)
        {
            var result = await _todoItemService.SearchTodoItem(filter,op);
            return Ok(result);
        }


        /// <summary>
        /// Method to Update TodoItem based on given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTodoItem([FromQuery]long id, [FromQuery]ToDoItem todoItem)
        {
            if (id != todoItem.ItemId)
            {
                return BadRequest();
            }

            await _todoItemService.UpdateTodoItem(id,todoItem);
            return NoContent();
        }

        /// <summary>
        // /// Method to Patch TodoItem based on given ID
        // /// </summary>
        // /// <param name="id"></param>
        // /// <param name="todoItem"></param>
        // /// <returns></returns>
        // [HttpPatch]
        // [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        // [Route("PatchToDoItem")]
        // public async Task<IActionResult> JsonPatchTodoItem(long id, [FromBody] JsonPatchDocument<TodoItem> todoItem)
        // {
        //     await _todoItemService.PatchTodoItem(id, todoItem);
        //     return NoContent();
            
        // }
        /// <summary>
        /// Method to create a TodoItem
        /// </summary>
        /// <param name="todoItem"></param>
        /// <returns>TodoItem</returns>
        [HttpPost]
        //[Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ToDoItem>> PostTodoItem([FromQuery]ToDoItem todoItem)
        {
            if (todoItem.Description == null) 
                return BadRequest(new {message="TodoItem Description mandatory" });
            
            await _todoItemService.CreateTodoItem(todoItem);
            return CreatedAtAction("GetTodoItem", new { id = todoItem.ItemId }, todoItem);
        }

        /// <summary>
        /// Method to delete TodoItem of given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        
        public async Task<IActionResult> DeleteTodoItem([FromQuery]long id)
        {
            await _todoItemService.DeleteTodoItem(id);
            return NoContent();
        }
    }
}
