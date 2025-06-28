using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using MtuSetsAPIs.Global;
using MtuSetsAPIs.Models.Teacher;
using Microsoft.AspNetCore.Authorization;

namespace MtuSetsAPIs.Controllers
{
    [Route("api/Teachers")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        /// <summary>
        /// Retrieves a list of all teachers from the system.
        /// </summary>
        /// <returns>A list of TeacherDTO objects if found, otherwise a 404 NotFound response with an error message.
        /// or a 500 InternalServerError if any internal error occurred.
        /// </returns>

        [HttpGet("All", Name = "GetAllTeacher")]
        [Authorize(Roles = "3953")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<TeacherDTO>> GetAllTeacher()
        {
            try
            {
                List<DataAccessLayer.TeacherDTO> TeacherList = BusinessLayer.Teacher.GetAllTeacher();
                if (TeacherList.Count == 0)
                {
                    return NotFound("No Teacher Found!");
                }
                return Ok(TeacherList); // Returns the list of students.
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        /// <summary>
        /// Retrieves a specific teacher by their ID.
        /// </summary>
        /// <param name="id">The ID of the teacher to retrieve.</param>
        /// <returns>
        /// A TeacherDTO object if the teacher is found, a 400 BadRequest if the ID is invalid,
        /// a 404 NotFound if the teacher with the given ID does not exist  or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpGet("{id}", Name = "GetTeacherById")]
        [Authorize(Roles = "3953,9763")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DataAccessLayer.TeacherDTO> GetTeacherById(int id)
        {
            try {
                if (id < 1)
                {
                    return BadRequest($"Not accepted ID {id}");
                }


                var Teacher = BusinessLayer.Teacher.Find(id);

                if (Teacher == null)
                {
                    return NotFound($"Teacher with ID {id} not found.");
                }

                //here we get only the DTO object to send it back.
                DataAccessLayer.TeacherDTO TDTO = Teacher.TDTO;

                //we return the DTO not the student object.
                return Ok(TDTO);
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Adds a new teacher to the system.
        /// </summary>
        /// <param name="newTeacherDTO">The TeacherDTO object containing the new teacher's details.</param>
        /// <returns>
        /// A 201 Created response with the newly created teacher's details if the data is valid, 
        /// a 400 BadRequest response if the data is invalid or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpPost(Name = "AddTeacher")]
        [Authorize(Roles = "3953")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DataAccessLayer.TeacherDTO>> AddTeacher(dtoAdd newTeacherDTO)
        {
            try {
                //we validate the data here
                if (newTeacherDTO == null || string.IsNullOrEmpty(newTeacherDTO.Name)
                    || string.IsNullOrEmpty(newTeacherDTO.Email) || string.IsNullOrEmpty(newTeacherDTO.Password)
                    || string.IsNullOrEmpty(newTeacherDTO.Phone) || string.IsNullOrEmpty(newTeacherDTO.Role)
                    || newTeacherDTO.Image == null || string.IsNullOrEmpty(newTeacherDTO.DepartmentId))
                {
                    return BadRequest("Invalid Teacher data.");
                }

                var imagepath = await CloudinaryService.UploadImageAsync(newTeacherDTO.Image);

                BusinessLayer.Teacher Teacher = new BusinessLayer.Teacher(new TeacherDTO(newTeacherDTO.Id, newTeacherDTO.Name,
                    newTeacherDTO.Email, newTeacherDTO.Password, newTeacherDTO.Phone, imagepath,
                    newTeacherDTO.Role, newTeacherDTO.DepartmentId));
                Teacher.Save();

                dtoAdded h = new dtoAdded(Teacher);


                //we return the DTO only not the full student object
                //we dont return Ok here,we return createdAtRoute: this will be status code 201 created.
                return CreatedAtRoute("GetTeacherById", new { id = newTeacherDTO.Id }, h);
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }

        }



        /// <summary>
        /// Updates exists teacher in the system.
        /// </summary>
        /// <param name="id">The Id of the teacher that will be updated.</param>
        /// <param name="updatedTeacher">The TeacherDTO object containing the new teacher's details.</param>
        /// <returns>
        /// A 201 Created response with the newly created teacher's details if the data is valid, 
        /// a 400 BadRequest response if the data is invalid or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpPut("{id}", Name = "UpdateTeacher")]
        [Authorize(Roles = "3953")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DataAccessLayer.TeacherDTO>> UpdateTeacher(int id,dtoAdd updatedTeacher)
        {
            try {
                if (id < 1 || updatedTeacher == null || string.IsNullOrEmpty(updatedTeacher.Name)
                || string.IsNullOrEmpty(updatedTeacher.Email) || string.IsNullOrEmpty(updatedTeacher.Password)
                || string.IsNullOrEmpty(updatedTeacher.Phone) || updatedTeacher.Image == null
                || string.IsNullOrEmpty(updatedTeacher.Role) || string.IsNullOrEmpty(updatedTeacher.DepartmentId))
                {
                    return BadRequest("Invalid Teacher data.");
                }


                var Teacher = BusinessLayer.Teacher.Find(id);


                if (Teacher == null)
                {
                    return NotFound($"Teacher with ID {id} not found.");
                }


                Teacher.Name = updatedTeacher.Name;
                Teacher.Email = updatedTeacher.Email;
                Teacher.Password = BusinessLayer.Teacher.ComputeHash(updatedTeacher.Password);
                Teacher.Phone = updatedTeacher.Phone;
                Teacher.Role = updatedTeacher.Role;
                Teacher.DepartmentId = updatedTeacher.DepartmentId;

                Teacher.ImagePath = await CloudinaryService.ReplaceImageAsync(updatedTeacher.Image, Teacher.ImagePath);


                Teacher.Save();

                //we return the DTO not the full student object.
                return Ok(Teacher.TDTO);
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        /// <summary>
        /// Deletes a specific teacher by their ID.
        /// </summary>
        /// <param name="id">The ID of the teacher to delete.</param>
        /// <returns>
        /// A 200 OK response if the teacher is successfully deleted, a 400 BadRequest if the ID is invalid,
        /// a 404 NotFound if the teacher with the given ID does not exist  or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpDelete("{id}", Name = "DeleteTeacher")]
        [Authorize(Roles = "3953")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteTeacher(int id)
        {
            try {
                if (id < 1)
                {
                    return BadRequest($"Not accepted ID {id}");
                }

                var Teacher = BusinessLayer.Teacher.Find(id);

                if (Teacher == null)
                {
                    return NotFound($"Teacher with ID {id} not found.");
                }

                await CloudinaryService.DeleteImageAsync(Teacher.ImagePath);

                if (BusinessLayer.Teacher.DeleteTeacher(id))
                    return Ok($"Teacher with ID {id} has been deleted.");
                else
                    return NotFound($"Teacher with ID {id} not found. No rows deleted!");
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        /// <summary>
        /// Retrieves a specific teacher name by their ID.
        /// </summary>
        /// <param name="id">The ID of the teacher to retrieve their name.</param>
        /// <returns>
        /// A 200 OK response if the teacher is successfully deleted, a 400 BadRequest if the ID is invalid
        /// ,a 404 NotFound if the teacher with the given ID does not exist or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpGet("Name/{id}", Name = "GetTeacherName")]
        [Authorize(Roles = "1753")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> GetTeacherName(int id)
        {
            try {
                if (id < 1)
                {
                    return BadRequest($"Not accepted ID {id}");
                }


                string name = BusinessLayer.Teacher.GetTeacherName(id);

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


        /// <summary>
        /// Retrieves teacher image by their ID.
        /// </summary>
        /// <param name="id">The ID of the teacher to retrieve image.</param>
        /// <returns>
        /// A image path if the teacher is found, a 400 BadRequest if the ID is invalid,
        /// a 404 NotFound if the student with the given ID does not exist  or a 500 InternalServerError if any internal error occurred.
        /// </returns>
        [HttpGet("image/{id}", Name = "GetTeacherImage")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> GetTeacherImage(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest($"Not accepted ID {id}");
                }
                var image = BusinessLayer.Teacher.GetTeacherImage(id);

                if (string.IsNullOrEmpty(image))
                {
                    return NotFound($"Teacher image with ID {id} not found.");
                }
                return Ok(image);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
