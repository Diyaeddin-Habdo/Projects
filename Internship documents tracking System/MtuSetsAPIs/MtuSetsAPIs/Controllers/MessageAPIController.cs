using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MtuSetsAPIs.Controllers
{
    [Route("api/Messages")]
    [ApiController]
    public class MessageAPIController : ControllerBase
    {

        /// <summary>
        /// Retrieves messages sent to student by their ID.
        /// </summary>
        /// <param name="id">The ID of the student to retrieve messages.</param>
        /// <returns>
        /// A MessageDTO object if the teacher is found, a 400 BadRequest if the ID is invalid,
        /// a 404 NotFound if the teacher with the given ID does not exist  or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpGet("{id}", Name = "GetMessageByToId")]
        [Authorize(Roles = "1753")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<MessageDTO>> GetMessagesByToId(int id)
        {
            try {
                if (id < 1)
                {
                    return BadRequest($"Not accepted ID {id}");
                }


                var messagesList = BusinessLayer.Message.GetMessagesByToId(id);

                if (messagesList.Count == 0)
                {
                    return NotFound($"No Messages found.");
                }


                return Ok(messagesList);
            }
            catch { 
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        /// <summary>
        /// Retrieves messages these sent by specific teacher by their ID.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <returns>
        /// A MessageDTO object if the teacher is found, a 400 BadRequest if the ID is invalid,
        /// a 404 NotFound if the teacher with the given ID does not exist  or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpGet("From/{id}", Name = "GetMessageByFromId")]
        [Authorize(Roles = "3953,9763")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<MessageDTO>> GetMessageByFromId(int id)
        {
            try {
                if (id < 1)
                {
                    return BadRequest($"Not accepted ID {id}");
                }


                var messagesList = BusinessLayer.Message.GetMessageByFromId(id);

                if (messagesList.Count == 0)
                {
                    return NotFound($"No Messages found.");
                }


                return Ok(messagesList);
            }
            catch { 
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


    }
}
