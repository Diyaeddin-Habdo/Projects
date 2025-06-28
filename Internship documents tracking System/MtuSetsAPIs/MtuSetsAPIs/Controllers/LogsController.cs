using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MtuSetsAPIs.Controllers
{
    [Route("api/Logs")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        [HttpGet("{teahcerId}", Name = "GetTeacherApprovalLogs")]
        [Authorize(Roles = "3953,9763")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<TeacherApprovalLogsDTO>> GetTeacherApprovalLogs(int teahcerId)
        {

            if (teahcerId < 1)
            {
                return BadRequest($"Not accepted ID {teahcerId}");
            }


            var logs = BusinessLayer.Logs.GetTeacherApprovalLogs(teahcerId);

            if (logs.Count == 0)
            {
                return NotFound($"No Logs found.");
            }


            return Ok(logs);

        }


        [HttpGet("Student/{studentId}", Name = "GetTeacherApprovalLogsByStudentId")]
        [Authorize(Roles = "1753")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<TeacherApprovalLogsForStudentDTO>> GetTeacherApprovalLogsByStudentId(int studentId)
        {

            if (studentId < 1)
            {
                return BadRequest($"Not accepted ID {studentId}");
            }


            var logs = BusinessLayer.Logs.GetTeacherApprovalLogsByStudentId(studentId);

            if (logs.Count == 0)
            {
                return NotFound($"No Logs found.");
            }


            return Ok(logs);

        }
    }
}
