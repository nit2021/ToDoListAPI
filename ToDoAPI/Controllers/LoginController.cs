using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoListAPI.ToDoAPI.DTO;
using ToDoListAPI.ToDoAPI.Services;

namespace ToDoListAPI.ToDoAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// The user service
        /// </summary>
        private IUserService _userservice;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public LoginController(IUserService userService)
        {
            _userservice = userService;
        }

        /// <summary>
        /// Authenticates the user
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Login User")]
        [HttpGet("/api/User")]
        public ActionResult<UsersDTO> AuthenticateUser(string username, string password)
        {
            if (username == null || password == null)
                return BadRequest();
            var IsValid = _userservice.ValidateCredentials(username, password);
            if (IsValid)
                return Ok();
            else
                return Unauthorized();
        }

    }
}
