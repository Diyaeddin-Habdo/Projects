using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MtuSetsAPIs.Models.Auth;
using MtuSetsAPIs.Models.Teacher;

namespace MtuSetsAPIs.Controllers
{
    [Route("api/Teachers/Auth")]
    [ApiController]
    public class TeacherAuthAPIController : ControllerBase
    {

        private IConfiguration _Config;
        public TeacherAuthAPIController(IConfiguration configuration)
        {
            _Config = configuration;
        }


        /// <summary>
        /// Authenticates a user based on their email and password.
        /// </summary>
        /// <param name="loginInfo">The login information containing the user's email and password.</param>
        /// <returns>
        /// An ActionResult containing a dtoAuth object if authentication is successful; 
        /// otherwise, returns a 404 Not Found status with a message indicating the user was not found.Or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("Login", Name = "Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<dtoAuth> Login([FromBody] dtoLogin loginInfo)
        {
            try {
                var user = Teacher.Login(loginInfo.Email, loginInfo.Password, _Config);

                if (user != null)
                {
                    return Ok(user);
                }

                return NotFound("User not found");
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }



    }
}
