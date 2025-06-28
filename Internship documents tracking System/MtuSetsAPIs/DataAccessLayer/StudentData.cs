using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string SNo { get; set; }
        public string ImagePath { get; set; }
        public string Role { get; set; }
        public string DepartmentId { get; set; }

        

        public StudentDTO(int id, string name, string email, string password, string phone,string SNo, string imagePath,string role,string departmentID)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Phone = phone;
            this.SNo = SNo;
            this.ImagePath = imagePath;
            this.Role = role;
            this.DepartmentId = departmentID;

        }
    }
    public class StudentData
    {
        /// <summary>
        /// Retrieves a list of all students from the database.
        /// </summary>
        /// <returns>
        /// A list of <see cref="StudentDTO"/> objects containing information for each student in the database.
        /// </returns>
        public static List<StudentDTO> GetAllStudents()
        {
            var StudentsList = new List<StudentDTO>();

            using (SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAllStudents", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new StudentDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetString(reader.GetOrdinal("Email")),
                                    reader.GetString(reader.GetOrdinal("Password")),
                                    reader.GetString(reader.GetOrdinal("Phone")),
                                    reader.GetString(reader.GetOrdinal("SNo")),
                                    reader.GetString(reader.GetOrdinal("ImagePath")),
                                    reader.GetString(reader.GetOrdinal("Role")),
                                    reader.GetString(reader.GetOrdinal("DepartmentId"))
                                ));
                            }
                        }
                    }
                    catch(DataAccessException ex) 
                    {
                        NlogSettings.LogError(ex, "Error in GetAllStudents method.");
                        throw new DataAccessException("An error occurred while retrieving teachers.", ex);
                    }
                }


                return StudentsList;
            }

        }

        /// <summary>
        /// Retrieves a student's information from the database based on the specified student ID.
        /// </summary>
        /// <param name="StudentId">The unique identifier of the student.</param>
        /// <returns>
        /// A <see cref="StudentDTO"/> object containing the student's information if found; otherwise, null.
        /// </returns>
        public static StudentDTO? GetStudentById(int StudentId)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetStudentById", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StudentId", StudentId);

                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new StudentDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetString(reader.GetOrdinal("Email")),
                                    reader.GetString(reader.GetOrdinal("Password")),
                                    reader.GetString(reader.GetOrdinal("Phone")),
                                    reader.GetString(reader.GetOrdinal("SNo")),
                                    reader.GetString(reader.GetOrdinal("ImagePath")),
                                    reader.GetString(reader.GetOrdinal("Role")),
                                    reader.GetString(reader.GetOrdinal("DepartmentId"))
                                );
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch(DataAccessException ex) 
                    {
                        NlogSettings.LogError(ex, "Error in GetStudentById method.");
                        throw new DataAccessException("An error occurred while retrieving teachers.", ex);
                    }
                }
            }
        }


        public static string? GetStudentImage(int studentId)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetStudentImage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentId", studentId);

                    try
                    {
                        connection.Open();

                        // Execute the command and retrieve the ImagePath
                        var result = command.ExecuteScalar(); // This will return the first column of the first row

                        // Check if result is not null and cast it to string
                        return result != null ? result.ToString() : null;
                    }
                    catch (SqlException ex)
                    {
                        NlogSettings.LogError(ex, "Error in GetStudentImage method.");
                        throw new DataAccessException("An error occurred while retrieving the student image.", ex);
                    }
                }
            }
        }


        /// <summary>
        /// Adds a new student to the database using the provided student data transfer object (DTO).
        /// </summary>
        /// <param name="StudentDTO">The <see cref="StudentDTO"/> object containing the student's information.</param>
        /// <returns>
        /// The unique identifier of the newly added student; 
        /// returns -1 if the operation fails.
        /// </returns>
        public static int AddStudent(StudentDTO StudentDTO)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_AddStudent", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Name", StudentDTO.Name);
                        command.Parameters.AddWithValue("@Email", StudentDTO.Email);
                        command.Parameters.AddWithValue("@Password", StudentDTO.Password);
                        command.Parameters.AddWithValue("@Phone", StudentDTO.Phone);
                        command.Parameters.AddWithValue("@SNo", StudentDTO.SNo);
                        command.Parameters.AddWithValue("@ImagePath", StudentDTO.ImagePath);
                        command.Parameters.AddWithValue("@DepartmentId", StudentDTO.DepartmentId);
                      
                        var outputIdParam = new SqlParameter("@NewStudentId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        return (int)outputIdParam.Value;
                    }
                    catch(DataAccessException ex)
                    {
                        NlogSettings.LogError(ex, "Error in AddStudent method.");
                        throw new DataAccessException("An error occurred while retrieving teachers.", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the information of an existing student in the database using the provided student data transfer object (DTO).
        /// </summary>
        /// <param name="StudentDTO">The <see cref="StudentDTO"/> object containing the updated student's information.</param>
        /// <returns>
        /// True if the update operation is successful; otherwise, false.
        /// </returns>
        public static bool UpdateStudent(StudentDTO StudentDTO)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_UpdateStudent", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Id", StudentDTO.Id);
                        command.Parameters.AddWithValue("@Name", StudentDTO.Name);
                        command.Parameters.AddWithValue("@Email", StudentDTO.Email);
                        command.Parameters.AddWithValue("@Password", StudentDTO.Password);
                        command.Parameters.AddWithValue("@Phone", StudentDTO.Phone);
                        command.Parameters.AddWithValue("@SNo", StudentDTO.SNo);
                        command.Parameters.AddWithValue("@ImagePath", StudentDTO.ImagePath);
                        command.Parameters.AddWithValue("@DepartmentId", StudentDTO.DepartmentId);

                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch(DataAccessException ex)
                    {
                        NlogSettings.LogError(ex, "Error in UpdateStudent method.");
                        throw new DataAccessException("An error occurred while retrieving teachers.", ex);
                    }

                }
            }
        }

        /// <summary>
        /// Deletes a student from the database based on the specified student ID.
        /// </summary>
        /// <param name="StudentId">The unique identifier of the student to be deleted.</param>
        /// <returns>
        /// True if the deletion is successful and exactly one row is affected; otherwise, false.
        /// </returns>
        public static bool DeleteStudent(int StudentId)
        {
            int rowsAffected = 0;
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_DeleteStudent", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StudentId", StudentId);

                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();
                        return (rowsAffected == 1);
                    }
                    catch(DataAccessException ex)
                    {
                        NlogSettings.LogError(ex, "Error in DeleteStudent method.");
                        throw new DataAccessException("An error occurred while retrieving teachers.", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves a student's information from the database based on the provided email and password.
        /// </summary>
        /// <param name="email">The email address of the student.</param>
        /// <param name="password">The password of the student.</param>
        /// <returns>
        /// A <see cref="StudentDTO"/> object containing the student's information if the credentials are valid; otherwise, null.
        /// </returns>
        public static StudentDTO? FindByCredentials(string email, string password)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_FindStudentByCredentials", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new StudentDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetString(reader.GetOrdinal("Email")),
                                    reader.GetString(reader.GetOrdinal("Password")),
                                    reader.GetString(reader.GetOrdinal("Phone")),
                                    reader.GetString(reader.GetOrdinal("SNo")),
                                    reader.GetString(reader.GetOrdinal("ImagePath")),
                                    reader.GetString(reader.GetOrdinal("Role")),
                                    reader.GetString(reader.GetOrdinal("DepartmentId"))
                                );
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch(DataAccessException ex)
                    {
                        NlogSettings.LogError(ex, "Error in FindStudent method.");
                        throw new DataAccessException("An error occurred while retrieving teachers.", ex);
                    }
                }
            }
        }
        
       
        /// <summary>
        /// Retrieves a list of students with pending statuses from the database.
        /// </summary>
        /// <returns>
        /// A list of <see cref="StudentDTO"/> objects containing information for each pending student.
        /// </returns>
        public static List<StudentDTO> GetPendingStudents(string DepartmentId)
        {
            var StudentsList = new List<StudentDTO>();

            using (SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetPendingStudents", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new StudentDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetString(reader.GetOrdinal("Email")),
                                    reader.GetString(reader.GetOrdinal("Password")),
                                    reader.GetString(reader.GetOrdinal("Phone")),
                                    reader.GetString(reader.GetOrdinal("SNo")),
                                    reader.GetString(reader.GetOrdinal("ImagePath")),
                                    reader.GetString(reader.GetOrdinal("Role")),
                                    reader.GetString(reader.GetOrdinal("DepartmentId"))
                                ));
                            }
                        }
                    }
                    catch(DataAccessException ex) 
                    {
                        NlogSettings.LogError(ex, "Error in GetPendingStudents method.");
                        throw new DataAccessException("An error occurred while retrieving teachers.", ex);
                    }
                }


                return StudentsList;
            }

        }

        /// <summary>
        /// Retrieves a list of students awaiting teacher approval from the database.
        /// </summary>
        /// <returns>
        /// A list of <see cref="StudentDTO"/> objects containing information for each student pending teacher approval.
        /// </returns>
        public static List<StudentDTO> GetTeacherApprovalStudents(string DepartmentId)
        {
            var StudentsList = new List<StudentDTO>();

            using (SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetTeacherApprovalStudents", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new StudentDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetString(reader.GetOrdinal("Email")),
                                    reader.GetString(reader.GetOrdinal("Password")),
                                    reader.GetString(reader.GetOrdinal("Phone")),
                                    reader.GetString(reader.GetOrdinal("SNo")),
                                    reader.GetString(reader.GetOrdinal("ImagePath")),
                                    reader.GetString(reader.GetOrdinal("Role")),
                                    reader.GetString(reader.GetOrdinal("DepartmentId"))
                                ));
                            }
                        }
                    }
                    catch(DataAccessException ex) 
                    {
                        NlogSettings.LogError(ex, "Error in GetTeacherApprovalStudents method.");
                        throw new DataAccessException("An error occurred while retrieving teachers.", ex);
                    }
                }


                return StudentsList;
            }

        }

        /// <summary>
        /// Retrieves a list of students awaiting advisor approval from the database.
        /// </summary>
        /// <returns>
        /// A list of <see cref="StudentDTO"/> objects containing information for each student pending advisor approval.
        /// </returns>
        public static List<StudentDTO> GetAdvisorApprovalStudents(string DepartmentId)
        {
            var StudentsList = new List<StudentDTO>();

            using (SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAdvisorApprovalStudents", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new StudentDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetString(reader.GetOrdinal("Email")),
                                    reader.GetString(reader.GetOrdinal("Password")),
                                    reader.GetString(reader.GetOrdinal("Phone")),
                                    reader.GetString(reader.GetOrdinal("SNo")),
                                    reader.GetString(reader.GetOrdinal("ImagePath")),
                                    reader.GetString(reader.GetOrdinal("Role")),
                                    reader.GetString(reader.GetOrdinal("DepartmentId"))
                                ));
                            }
                        }
                    }
                    catch(DataAccessException ex) 
                    {
                        NlogSettings.LogError(ex, "Error in GetAdvisorApprovalStudents method.");
                        throw new DataAccessException("An error occurred while retrieving teachers.", ex);
                    }
                }


                return StudentsList;
            }

        }

        /// <summary>
        /// Retrieves a list of students who are not approved by the teacher from the database.
        /// </summary>
        /// <returns>
        /// A list of <see cref="StudentDTO"/> objects representing students that are Unapproved.
        /// </returns>
        public static List<StudentDTO> GetTeacherUnapprovalStudents(string DepartmentId)
        {
            var StudentsList = new List<StudentDTO>();

            using (SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetTeacherUnapprovalStudents", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new StudentDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetString(reader.GetOrdinal("Email")),
                                    reader.GetString(reader.GetOrdinal("Password")),
                                    reader.GetString(reader.GetOrdinal("Phone")),
                                    reader.GetString(reader.GetOrdinal("SNo")),
                                    reader.GetString(reader.GetOrdinal("ImagePath")),
                                    reader.GetString(reader.GetOrdinal("Role")),
                                    reader.GetString(reader.GetOrdinal("DepartmentId"))
                                ));
                            }
                        }
                    }
                    catch(DataAccessException ex) 
                    {
                        NlogSettings.LogError(ex, "Error in GetTeacherUnapprovalStudents method.");
                        throw new DataAccessException("An error occurred while retrieving teachers.", ex);
                    }
                }


                return StudentsList;
            }

        }

        /// <summary>
        /// Retrieves a list of students who are not approved by the advisor from the database.
        /// </summary>
        /// <returns>
        /// A list of <see cref="StudentDTO"/> objects representing students that are Unapproved.
        /// </returns>
        public static List<StudentDTO> GetAdvisorUnapprovalStudents(string DepartmentId)
        {
            var StudentsList = new List<StudentDTO>();

            using (SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAdvisorUnapprovalStudents", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DepartmentId", DepartmentId);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsList.Add(new StudentDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetString(reader.GetOrdinal("Email")),
                                    reader.GetString(reader.GetOrdinal("Password")),
                                    reader.GetString(reader.GetOrdinal("Phone")),
                                    reader.GetString(reader.GetOrdinal("SNo")),
                                    reader.GetString(reader.GetOrdinal("ImagePath")),
                                    reader.GetString(reader.GetOrdinal("Role")),
                                    reader.GetString(reader.GetOrdinal("DepartmentId"))
                                ));
                            }
                        }
                    }
                    catch(DataAccessException ex) 
                    {
                        NlogSettings.LogError(ex, "Error in GetAdvisorUnapprovalStudents method.");
                        throw new DataAccessException("An error occurred while retrieving teachers.", ex);
                    }
                }


                return StudentsList;
            }

        }

        /// <summary>
        /// Retrieves the name of a student based on their ID from the database.
        /// </summary>
        /// <param name="studentId">The ID of the student whose name is to be retrieved.</param>
        /// <returns>
        /// The name of the student if found; otherwise, an empty string.
        /// </returns>
        public static string GetStudentName(int studentId)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetStudentName", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StudentId", studentId);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return (string)reader["Name"];
                            }
                            else
                            {
                                return "";
                            }
                        }
                    }
                    catch(DataAccessException ex) 
                    {
                        NlogSettings.LogError(ex, "Error in GetStudentName method.");
                        throw new DataAccessException("An error occurred while retrieving teachers.", ex);
                    }
                }
            }
        }

        public static StudentDTO? Login(string email, string password)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_StudentLogin", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);

                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new StudentDTO
                                (
                                        reader.GetInt32(reader.GetOrdinal("Id")),
                                        reader.GetString(reader.GetOrdinal("Name")),
                                        reader.GetString(reader.GetOrdinal("Email")),
                                        reader.GetString(reader.GetOrdinal("Password")),
                                        reader.GetString(reader.GetOrdinal("Phone")),
                                        reader.GetString(reader.GetOrdinal("SNo")),
                                        reader.GetString(reader.GetOrdinal("ImagePath")),
                                        reader.GetString(reader.GetOrdinal("Role")),
                                        reader.GetString(reader.GetOrdinal("DepartmentId"))
                                );
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        NlogSettings.LogError(ex, "Error in FindByCredentials method.");
                        throw new DataAccessException("An error occurred while finding teacher.", ex);
                    }
                }
            }
        }
    }
}
