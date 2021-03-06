using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoAPI.Core.Models;
using ToDoAPI.Core.Utilities;
using ToDoListAPI.ToDoBLL.DTO;
using ToDoListAPI.ToDoBLL.Services;

namespace ToDoListAPI.ToDoAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoService _todoItemService;
        private readonly IMapper _mapper;
        public ToDoItemController(IToDoService todoItemService, IMapper mapper)
        {
            _todoItemService = todoItemService;
            _mapper = mapper;
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
        public async Task<ActionResult<IEnumerable<ToDoItemDTO>>> GetTodoItemList([FromQuery] OwnerParameters options)
        {
            var todoItems = await _todoItemService.GetAllTodoItem(options);
            if (todoItems == null)
            {
                return NotFound(new { message = "TodoItem does not exists" });
            }
            var toDoItemsDTO = _mapper.Map<IEnumerable<ToDoItem>, IEnumerable<ToDoItemDTO>>(todoItems);
            return Ok(toDoItemsDTO);
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
        public async Task<ActionResult<ToDoItemDTO>> GetTodoItem(int id)
        {
            var todoItem = await _todoItemService.GetTodoItemById(id);

            if (todoItem == null)
            {
                return NotFound(new { message = "Todo Item does not exists" });
            }
            var toDoItemDTO = _mapper.Map<ToDoItem, ToDoItemDTO>(todoItem);
            return Ok(toDoItemDTO);
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
        [ProducesResponseType(typeof(ToDoItemDTO), (int)HttpStatusCode.OK)]
        [SwaggerOperation(Summary = "Search TodoItem based on Search Filter")]
        [Route("SearchToDoItem")]
        public async Task<ActionResult<ToDoItemDTO>> SearchTodoItem([FromQuery] string filter, [FromQuery] OwnerParameters op)
        {
            var todoItems = await _todoItemService.SearchTodoItem(filter, op);
            if (todoItems == null)
                return NotFound();
            else
            {
                var toDoItemsDTO = _mapper.Map<IEnumerable<ToDoItem>, IEnumerable<ToDoItemDTO>>(todoItems);
                return Ok(toDoItemsDTO);
            }
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(Summary = "Update TodoItem based on given ID")]
        public async Task<IActionResult> PutTodoItem([FromBody] ToDoItemUpDTO itemUpDTO)
        {
            if (itemUpDTO.Description == null || itemUpDTO.ItemId == 0)
                return BadRequest();

            var result = await _todoItemService.UpdateTodoItem(itemUpDTO);

            if (result == null) return NotFound(); else return NoContent();
        }

        // <summary>
        /// Method to Patch TodoItem based on given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        [HttpPatch("{itemId:long}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(Summary = "Patch TodoItem based on given ID")]
        public async Task<IActionResult> JsonPatchTodoItem(long itemId, [FromBody] JsonPatchDocument<ToDoItem> todoItem)
        {
            var item = await _todoItemService.PatchTodoItem(itemId, todoItem);
            if (item == null)
                return BadRequest();
            else
            {
                var toDoItemDTO = _mapper.Map<ToDoItem, ToDoItemDTO>(item);
                return Ok(toDoItemDTO);
            }
        }

        /// <summary>
        /// Method to create a TodoItem
        /// </summary>
        /// <param name="ItemDesc"></param>
        /// <returns>TodoItem</returns>
        [HttpPost("PostTodoItem")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(Summary = "Create a TodoItem")]
        public async Task<ActionResult<ToDoItemDTO>> PostTodoItem([FromBody] ToDoItemInDTO itemInDTO)
        {
            if (itemInDTO.Description == null || itemInDTO.ToDoListID == 0)
                return BadRequest(new { message = "TodoItem Description mandatory" });

            ToDoItem item = await _todoItemService.CreateTodoItem(itemInDTO);
            var toDoItemDTO = _mapper.Map<ToDoItem, ToDoItemDTO>(item);
            return CreatedAtAction(nameof(GetTodoItem), new { Id = toDoItemDTO.ItemId }, toDoItemDTO);
        }

        /// <summary>
        /// Method to delete TodoItem of given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Delete TodoItem for given ID")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var deletedItem = await _todoItemService.DeleteTodoItem(id);
            if (deletedItem == null)
                return NotFound();
            else
                return NoContent();
        }

    }
}
