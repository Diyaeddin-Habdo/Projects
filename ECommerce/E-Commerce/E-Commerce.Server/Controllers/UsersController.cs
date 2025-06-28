using Business_layer;
using DataAccess_layer;
using DataAccess_layer.Models;
using E_Commerce.Server.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Server.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IConfiguration _Config;
        public UsersController(IConfiguration configuration)
        {
            _Config = configuration;
        }


        [AllowAnonymous]
        [HttpPost("Login",Name ="Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<dtoAuthenticatedUser> Login([FromBody]dtoUserLogin loginInfo)
        {

            var user = clsUser.Login(loginInfo.email,loginInfo.password, _Config);

            if(user != null)
            {
                return Ok(user);
            }

            return NotFound("User not found");
        }


        [AllowAnonymous]
        [HttpPost("Register", Name = "Register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<dtoAuthenticatedUser> Register(dtoRegister newUser)
        {

            if (newUser == null || string.IsNullOrEmpty(newUser.name)
                || string.IsNullOrEmpty(newUser.email) || string.IsNullOrEmpty(newUser.password))
            {
                return BadRequest("Invalid user data.");
            }

            var AuthenticatedUser = clsUser.Register(newUser.name, newUser.email, newUser.password,"User",_Config);

            if (AuthenticatedUser != null)
                return Ok(AuthenticatedUser);
            else
                return Conflict(new { message = "Email is already taken" });
        }

        



        [HttpGet(Name = "GetAllUsers")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<DataAccess_layer.Models.dtoUser>> GetAllUsers()
        {
            List<DataAccess_layer.Models.dtoUser> users = clsUser.GetAllUsers();
            if (users.Count == 0)
            {
                return NotFound("No user Found!");
            }
            return Ok(users); // Returns the list of students.
        }





        [HttpGet("{id}", Name = "GetUserById")]
        [Authorize(Roles = "Admin")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<DataAccess_layer.Models.dtoUser> GetUserById(int id)
        {

            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }
            DataAccess_layer.Models.dtoUser user = clsUser.Find(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }


            return Ok(user);

        }



        [HttpPost(Name = "AddUser")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataAccess_layer.Models.dtoUser> AddUser(dtoAddUser newUser)
        {

            if (newUser == null || string.IsNullOrEmpty(newUser.Name)
                || string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Password)
                || string.IsNullOrEmpty(newUser.roles))
            {
                return BadRequest("Invalid user data.");
            }
            clsUser user = new clsUser();
            user.name = newUser.Name;
            user.email = newUser.Email;
            user.password = newUser.Password;
            user.roles = newUser.roles;

            if (user.Save())
                return CreatedAtRoute("GetUserById", new { id = user.id }, new DataAccess_layer.Models.dtoUser(user.id,user.name,user.email, user.roles ));
            else
                return StatusCode(500, "An error accorded while adding the user.");
        }




        [HttpPut("{id}", Name = "UpdateUser")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateUser(int id, dtoUpdateUser updatedUser)
        {
            if (id < 1 || updatedUser == null || string.IsNullOrEmpty(updatedUser.name)
                || string.IsNullOrEmpty(updatedUser.email) || string.IsNullOrEmpty(updatedUser.roles))
            {
                return BadRequest("Invalid user data.");
            }

            clsUser user = clsUser.FindById(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }



            user.name = updatedUser.name;
            user.email = updatedUser.email;
            user.roles = updatedUser.roles;

            if (user.Save())
                return Ok(new {user.id,user.name,user.email,user.roles});
            else
                return StatusCode(500, "An error accorded while updating the user.");
        }



        //here we use HttpDelete method
        [HttpDelete("{id}", Name = "DeleteUser")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteUser(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            if (!clsUser.IsExists(id))
                return NotFound($"User with ID {id} not found. no rows deleted!");


            if (clsUser.DeleteUser(id))
                return Ok($"User with ID {id} has been deleted.");
            else
                return StatusCode(500, "An error accorded while deleting the user.");
        }
        private dtoCurrentUser GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new dtoCurrentUser
                {
                    name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    roles = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                };
            }

            return null;
        }
    }
}
