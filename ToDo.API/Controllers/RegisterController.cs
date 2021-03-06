using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoAPI.Core.Models;
using ToDoListAPI.ToDoBLL.DTO;
using ToDoListAPI.ToDoBLL.Services;

namespace ToDoListAPI.ToDoAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        /// <summary>
        /// The user service
        /// </summary>
        private IUserService _userservice;

        /// <summary>
        /// The class mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public RegisterController(IUserService userService, IMapper mapper)
        {
            _userservice = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(Summary = "Register new User")]
        [HttpPost]
        public async Task<ActionResult> RegisterUser([FromBody] UsersDTO user)
        {
            if (user.UserName == null || user.Password == null)
                return BadRequest();
            var newUser = await _userservice.CreateUser(user);
            var newUserDTO = _mapper.Map<Users, UsersDTO>(newUser);
            return Ok();
        }

    }
}
