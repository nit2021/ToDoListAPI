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
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoService _todoItemService;
        private readonly IMapper _mapper;
        public ToDoListController(IToDoService todoItemService, IMapper mapper)
        {
            _todoItemService = todoItemService;
            _mapper = mapper;
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
        public async Task<ActionResult<IEnumerable<ToDoListDTO>>> GetTodoListItem([FromQuery] OwnerParameters options)
        {
            var todoListItems = await _todoItemService.GetAllTodoList(options);
            if (todoListItems == null)
            {
                return NotFound(new { message = "TodoList Item does not exists" });
            }
            var toDoListDTO = _mapper.Map<IEnumerable<ToDoList>, IEnumerable<ToDoListDTO>>(todoListItems);
            return Ok(toDoListDTO);
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
        public async Task<ActionResult<ToDoListDTO>> GetTodoListItem(long id)
        {
            var todoListItem = await _todoItemService.GetTodoListById(id);

            if (todoListItem == null)
            {
                return NotFound(new { message = "TodoList Item does not exists" });
            }
            var toDoListDTO = _mapper.Map<ToDoList, ToDoListDTO>(todoListItem);
            return Ok(toDoListDTO);
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
        [ProducesResponseType(typeof(ToDoListDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Search TodoList Item based on Search Filter")]
        [Route("SearchToDoListItem")]
        public async Task<ActionResult<ToDoListDTO>> SearchTodoListItem([FromQuery] string filter, [FromQuery] OwnerParameters op)
        {
            var searchedList = await _todoItemService.SearchTodoList(filter, op);
            if (searchedList == null)
                return NotFound();
            else
            {
                var toDoListDTO = _mapper.Map<IEnumerable<ToDoList>, IEnumerable<ToDoListDTO>>(searchedList);
                return Ok(toDoListDTO);
            }
        }


        /// <summary>
        /// Method to Update TodoList Item based on given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoListDescription"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(Summary = "Update TodoList Item based on given ID")]
        public async Task<IActionResult> PutTodoListItem(ToDoListUpDTO toDoList)
        {
            if (toDoList.Description == null || toDoList.ListId == 0)
            {
                return BadRequest();
            }

            var result = await _todoItemService.UpdateTodoList(toDoList);
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
            {
                var toDoListDTO = _mapper.Map<ToDoList, ToDoListDTO>(item);
                return Ok(toDoListDTO);
            }
        }

        /// <summary>
        /// Method to create a TodoList Item
        /// </summary>
        /// <param name="ItemDesc"></param>
        /// <returns>TodoListItem</returns>
        [HttpPost("PostTodoListItem")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Creates a TodoList Item")]
        public async Task<ActionResult<ToDoListDTO>> PostTodoListItem([FromQuery] string ItemDesc)
        {
            if (ItemDesc == null)
                return BadRequest(new { message = "TodoList Item Description mandatory" });

            ToDoList item = await _todoItemService.CreateToDoList(ItemDesc);
            var toDoListDTO = _mapper.Map<ToDoList, ToDoListDTO>(item);
            return CreatedAtAction(nameof(GetTodoListItem), new { Id = toDoListDTO.ListId }, toDoListDTO);
        }

        /// <summary>
        /// Method to delete TodoList Item of given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Delete TodoList Item of given ID")]
        public async Task<IActionResult> DeleteTodoListItem(long id)
        {
            var deletedListItem = await _todoItemService.DeleteTodoList(id);
            if (deletedListItem == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
