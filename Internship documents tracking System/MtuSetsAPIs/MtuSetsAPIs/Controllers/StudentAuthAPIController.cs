using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MtuSetsAPIs.Models.Auth;

namespace MtuSetsAPIs.Controllers
{
    [Route("api/Students/Auth")]
    [ApiController]
    public class StudentAuthAPIController : ControllerBase
    {
        private IConfiguration _Config;
        public StudentAuthAPIController(IConfiguration configuration)
        {
            _Config = configuration;
        }

        /// <summary>
        /// Authenticates a user based on their email and password.
        /// </summary>
        /// <param name="loginInfo">The login information containing the user's email and password.</param>
        /// <returns>
        /// An ActionResult containing a dtoLogin object if authentication is successful; 
        /// otherwise, returns a 404 Not Found status with a message indicating the user was not found.Or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("Login", Name = "StudentLogin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentAuthData> StudentLogin([FromBody] dtoLogin loginInfo)
        {
            try {
                if (string.IsNullOrEmpty(loginInfo.Email) || string.IsNullOrEmpty(loginInfo.Password))
                    return BadRequest("Invalid data");
                var user = Student.Login(loginInfo.Email, loginInfo.Password, _Config);

                if (user != null)
                {
                    return Ok(user);
                }

                return NotFound("Student not found");
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
