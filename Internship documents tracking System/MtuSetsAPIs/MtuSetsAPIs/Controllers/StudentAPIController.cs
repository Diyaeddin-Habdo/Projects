using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MtuSetsAPIs.Global;
using MtuSetsAPIs.Models.Student;

namespace MtuSetsAPIs.Controllers
{
    [Route("api/Students")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        /// <summary>
        /// Retrieves a list of all students from the system.
        /// </summary>
        /// <returns>A list of StudentDTO objects if found, otherwise a 404 NotFound response with an error message.
        /// or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpGet("All", Name = "GetAllStudent")]
        [Authorize(Roles = "3953")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudent()
        {
            try {
                List<StudentDTO> StudentList = BusinessLayer.Student.GetAllStudent();
                if (StudentList.Count == 0)
                {
                    return NotFound("No Student Found!");
                }
                return Ok(StudentList); // Returns the list of students.
            }
            catch { 
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }



        /// <summary>
        /// Retrieves a specific student by their ID.
        /// </summary>
        /// <param name="id">The ID of the student to retrieve.</param>
        /// <returns>
        /// A StudentDTO object if the student is found, a 400 BadRequest if the ID is invalid,
        /// a 404 NotFound if the student with the given ID does not exist  or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpGet("{id}", Name = "GetStudentById")]
        [Authorize(Roles = "1753")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest($"Not accepted ID {id}");
                }
                var Student = BusinessLayer.Student.Find(id);

                if (Student == null)
                {
                    return NotFound($"Student with ID {id} not found.");
                }
                //here we get only the DTO object to send it back.
                StudentDTO SDTO = Student.SDTO;
                //we return the DTO not the student object.
                return Ok(SDTO);
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves student image by their ID.
        /// </summary>
        /// <param name="id">The ID of the student to retrieve image.</param>
        /// <returns>
        /// A image path if the student is found, a 400 BadRequest if the ID is invalid,
        /// a 404 NotFound if the student with the given ID does not exist  or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpGet("image/{id}", Name = "GetStudentImage")]
        [Authorize(Roles = "1753")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> GetStudentImage(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest($"Not accepted ID {id}");
                }
                var image = BusinessLayer.Student.GetStudentImage(id);

                if (string.IsNullOrEmpty(image))
                {
                    return NotFound($"Student image with ID {id} not found.");
                }
                return Ok(image);
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }



        /// <summary>
        /// Updates exists student in the system.
        /// </summary>
        /// <param name="id">The Id of the student that will be updated.</param>
        /// <param name="updatedStudent">The StudentDTO object containing the new student's details.</param>
        /// <returns>
        /// A 201 Created response with the newly created student's details if the data is valid, 
        /// a 400 BadRequest response if the data is invalid or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpPut("{id}", Name = "UpdateStudent")]
        [Authorize(Roles = "1753")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> UpdateStudent(int id, dtoAdd updatedStudent)
        {
            try {
                if (id < 1 || updatedStudent == null || string.IsNullOrEmpty(updatedStudent.Name)
                || string.IsNullOrEmpty(updatedStudent.Email) || string.IsNullOrEmpty(updatedStudent.Password)
                || string.IsNullOrEmpty(updatedStudent.Phone) || string.IsNullOrEmpty(updatedStudent.SNo)
                || updatedStudent.ImagePath == null || string.IsNullOrEmpty(updatedStudent.DepartmentId))
                {
                    return BadRequest("Invalid Student data.");
                }


                var Student = BusinessLayer.Student.Find(id);


                if (Student == null)
                {
                    return NotFound($"Student with ID {id} not found.");
                }


                Student.Name = updatedStudent.Name;
                Student.Email = updatedStudent.Email;
                Student.Password = BusinessLayer.Student.ComputeHash(updatedStudent.Password);
                Student.Phone = updatedStudent.Phone;
                Student.SNo = updatedStudent.SNo;
                Student.DepartmentId = updatedStudent.DepartmentId;

                Student.ImagePath = await CloudinaryService.ReplaceImageAsync(updatedStudent.ImagePath, Student.ImagePath);


                Student.Save();

                //we return the DTO not the full student object.
                return Ok(Student.SDTO);
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");

            }
        }


        /// <summary>
        /// Retrieves a list of all Pending Students from the system.
        /// </summary>
        /// <returns>A list of StudentDTO objects if found, otherwise a 404 NotFound response with an error message.
        /// or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpGet("Pending/{DepartmentId}", Name = "GetPendingStudents")]
        [Authorize(Roles = "9763")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetPendingStudents(string DepartmentId)
        {
            try {
                List<StudentDTO> StudentList = BusinessLayer.Student.GetPendingStudents(DepartmentId);
                if (StudentList.Count == 0)
                {
                    return NotFound("No Student Found!");
                }
                return Ok(StudentList); // Returns the list of students.
            }
            catch { 
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }



        /// <summary>
        /// Retrieves a list of all Teacher Approval Students from the system.
        /// </summary>
        /// <returns>A list of StudentDTO objects if found, otherwise a 404 NotFound response with an error message.
        /// or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpGet("TeacherApproval/{DepartmentId}", Name = "GetTeacherApprovalStudents")]
        [Authorize(Roles = "3953,9763")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetTeacherApprovalStudents(string DepartmentId)
        {
            try {
                List<StudentDTO> StudentList = BusinessLayer.Student.GetTeacherApprovalStudents(DepartmentId);
                if (StudentList.Count == 0)
                {
                    return NotFound("No Student Found!");
                }
                return Ok(StudentList); // Returns the list of students.
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        /// <summary>
        /// Retrieves a list of all Advisor Approval Students from the system.
        /// </summary>
        /// <returns>A list of StudentDTO objects if found, otherwise a 404 NotFound response with an error message.
        /// or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpGet("AdvisorApproval/{DepartmentId}", Name = "GetAdvisorApprovalStudents")]
        [Authorize(Roles = "3953")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetAdvisorApprovalStudents(string DepartmentId)
        {
            try
            {
                List<StudentDTO> StudentList = BusinessLayer.Student.GetAdvisorApprovalStudents(DepartmentId);
                if (StudentList.Count == 0)
                {
                    return NotFound("No Student Found!");
                }
                return Ok(StudentList); // Returns the list of students.
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves a list of all Teacher unapproval students from the system.
        /// </summary>
        /// <returns>A list of StudentDTO objects if found, otherwise a 404 NotFound response with an error message.
        /// or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpGet("TeacherUnapproval/{DepartmentId}", Name = "GetTeacherUnapprovalStudents")]
        [Authorize(Roles = "9763")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetTeacherUnapprovalStudents(string DepartmentId)
        {
            try {
                List<StudentDTO> StudentList = BusinessLayer.Student.GetTeacherUnapprovalStudents(DepartmentId);
                if (StudentList.Count == 0)
                {
                    return NotFound("No Student Found!");
                }
                return Ok(StudentList); // Returns the list of students.
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        /// <summary>
        /// Retrieves a list of all Advisor Unapproval Students from the system.
        /// </summary>
        /// <returns>A list of StudentDTO objects if found, otherwise a 404 NotFound response with an error message.
        /// or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpGet("AdvisorUnapproval/{DepartmentId}", Name = "GetAdvisorUnapprovalStudents")]
        [Authorize(Roles = "3953")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetAdvisorUnapprovalStudents(string DepartmentId)
        {
            try {
                List<StudentDTO> StudentList = BusinessLayer.Student.GetAdvisorUnapprovalStudents(DepartmentId);
                if (StudentList.Count == 0)
                {
                    return NotFound("No Student Found!");
                }
                return Ok(StudentList); // Returns the list of students.
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        /// <summary>
        /// Retrieves a specific student name by their ID.
        /// </summary>
        /// <param name="id">The ID of the student to retrieve their name.</param>
        /// <returns>
        /// The name of student if the student is found, a 400 BadRequest if the ID is invalid,
        /// a 404 NotFound if the student with the given ID does not exist  or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpGet("Name/{id}", Name = "GetStudentName")]
        [Authorize(Roles = "3953,9763")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> GetStudentName(int id)
        {
            try {
                if (id < 1)
                {
                    return BadRequest($"Not accepted ID {id}");
                }


                string name = BusinessLayer.Student.GetStudentName(id);

                if (name == "")
                {
                    return NotFound($"Teacher with ID {id} not found.");
                }

                return Ok(name);
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

    }
}
