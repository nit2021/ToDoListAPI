using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoAPI.Core.Models;
using ToDoListAPI.ToDoAPI.DTO;
using ToDoListAPI.ToDoAPI.Services;

namespace ToDoListAPI.ToDoAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ToDoLabelController : ControllerBase
    {
        private readonly IToDoService _todoItemService;
        private readonly IMapper _mapper;
        public ToDoLabelController(IToDoService todoItemService, IMapper mapper)
        {
            _todoItemService = todoItemService;
            _mapper = mapper;
        }

        /// <summary>
        /// Method to create a Label
        /// </summary>
        /// <param name="ItemDesc"></param>
        /// <param name="LabelDesc"></param>
        /// <returns>Label</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(Summary = "Creates a Label for a given Item Id")]
        public async Task<ActionResult<LabelDTO>> PostLabel([FromBody] LabelInDTO labelInDTO)
        {
            if (labelInDTO.Description == null || ((labelInDTO.ToDoItemID == 0) && (labelInDTO.ToDoListID == 0)))
                return BadRequest(new { message = "TodoItem Description and any of (ToDoItemID or ToDoListID) is mandatory" });

            Label label = await _todoItemService.CreateLabel(labelInDTO);
            if (label == null)
                return BadRequest();
            else
            {
                var labelsDTO = _mapper.Map<Label, LabelDTO>(label);
                if (labelsDTO.ToDoListID != 0)
                    return CreatedAtAction(nameof(GetLabelByToDoListID), new { Id = labelsDTO.ToDoListID }, labelsDTO);
                else
                    return CreatedAtAction(nameof(GetLabelByToDoItemID), new { Id = labelsDTO.ToDoItemID }, labelsDTO);
            }
        }

        /// <summary>
        /// Method to get List of All Label
        /// </summary>
        /// <param></param>
        /// <returns>List of Label</returns>
        [HttpGet("GetTodoLabel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get List of All Label")]
        public async Task<ActionResult<IEnumerable<LabelDTO>>> GetTodoLabel([FromQuery] OwnerParameters options)
        {
            var todoLabels = await _todoItemService.GetAllLabel(options);
            if (todoLabels == null)
            {
                return NotFound(new { message = "TodoItem does not exists" });
            }
            var labelsDTO = _mapper.Map<IEnumerable<Label>, IEnumerable<LabelDTO>>(todoLabels);
            return Ok(labelsDTO);
        }

        /// <summary>
        /// Method to get List of All Label by ToDoListID
        /// </summary>
        /// <param></param>
        /// <returns>List of Label by ToDoListID</returns>
        [HttpGet("GetLabelByToDoListID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get List of All Label by ToDoListID")]
        public async Task<ActionResult<IEnumerable<LabelDTO>>> GetLabelByToDoListID(int ToDoListID, [FromQuery] OwnerParameters options)
        {
            var todoLabels = await _todoItemService.GetAllLabelByToDoListID(ToDoListID, options);
            if (todoLabels == null)
            {
                return NotFound(new { message = "TodoItem does not exists" });
            }
            var labelsDTO = _mapper.Map<IEnumerable<Label>, IEnumerable<LabelDTO>>(todoLabels);
            return Ok(labelsDTO);
        }

        /// <summary>
        /// Method to get List of All Label by ToDoItemID
        /// </summary>
        /// <param></param>
        /// <returns>List of Label by ToDoItemID</returns>
        [HttpGet("GetLabelByToDoItemID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get List of All Label by ToDoItemID")]
        public async Task<ActionResult<IEnumerable<LabelDTO>>> GetLabelByToDoItemID([FromQuery] int ToDoItemID, [FromQuery] OwnerParameters options)
        {
            var todoLabels = await _todoItemService.GetAllLabelByToDoItemID(ToDoItemID, options);
            if (todoLabels == null)
            {
                return NotFound(new { message = "TodoItem does not exists" });
            }
            var labelsDTO = _mapper.Map<IEnumerable<Label>, IEnumerable<LabelDTO>>(todoLabels);
            return Ok(labelsDTO);
        }

        /// <summary>
        /// Method to delete Label of given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Delete Label of given ID")]
        public async Task<IActionResult> DeleteLabel(long id)
        {
            var deletedLabel = await _todoItemService.DeleteLabel(id);
            if (deletedLabel == null)
                return NotFound();
            else
                return NoContent();
        }
    }
}
