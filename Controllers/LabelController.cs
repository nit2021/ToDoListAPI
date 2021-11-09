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
    [Route("[controller]")]
    public class LabelController : ControllerBase
    {
        private readonly IToDoService _todoItemService;
        public LabelController(IToDoService todoItemService)
        {
            _todoItemService = todoItemService;
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
        /// Method to get List of All Label
        /// </summary>
        /// <param></param>
        /// <returns>List of Label</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Method to get List of All Label")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetTodoLabel([FromQuery] OwnerParameters options)
        {
            var todoItems = await _todoItemService.GetAllLabel(options);
            if (todoItems == null)
            {
                return NotFound(new { message = "TodoItem does not exists" });
            }
            return Ok(todoItems);
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
