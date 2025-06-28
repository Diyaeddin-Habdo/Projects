using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
    /// Represents a teacher's data transfer object (DTO) containing detailed information about a teacher.
    /// </summary>
    public class TeacherDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public string ImagePath { get; set; }

        public string Role { get; set; }
        public string DepartmentId { get; set; }
        public TeacherDTO(int id, string name, string email, string password, string phone, string imagePath, string role, string DepartmentId)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Phone = phone;
            this.ImagePath = imagePath;
            this.Role = role;
            this.DepartmentId = DepartmentId;

        }
    }

    // Teacher Data class
    public class TeacherData
    {
        /// <summary>
        /// Retrieves all teachers from the database using a stored procedure.
        /// </summary>
        /// <returns>
        /// A list of TeacherDTO objects representing all teachers in the database.
        /// If an error occurs during the database operation, an empty list is returned.
        /// </returns>
        public static List<TeacherDTO> GetAllTeachers()
        {
            var TeacherlarList = new List<TeacherDTO>();

            using (SqlConnection conn = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAllTeachers", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TeacherlarList.Add(new TeacherDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetString(reader.GetOrdinal("Email")),
                                    reader.GetString(reader.GetOrdinal("Password")),
                                    reader.GetString(reader.GetOrdinal("Phone")),
                                    reader.GetString(reader.GetOrdinal("ImagePath")),
                                    reader.GetString(reader.GetOrdinal("Role")),
                                    reader.GetString(reader.GetOrdinal("DepartmentId"))
                                ));
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        NlogSettings.LogError(ex, "Error in GetAllTeachers method.");
                        throw new DataAccessException("An error occurred while retrieving teachers.", ex);
                    }
                }
                return TeacherlarList;
            }
        }

        /// <summary>
        /// Retrieves a teacher's details from the database by their ID using a stored procedure.
        /// </summary>
        /// <param name="TeacherId">The ID of the teacher to retrieve.</param>
        /// <returns>
        /// A TeacherDTO object containing the teacher's details if found; otherwise, null.
        /// If an error occurs during the database operation, null is returned.
        /// </returns>
        public static TeacherDTO? GetTeacherById(int TeacherId)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetTeacherById", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TeacherId", TeacherId);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new TeacherDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                        reader.GetString(reader.GetOrdinal("Name")),
                                        reader.GetString(reader.GetOrdinal("Email")),
                                        reader.GetString(reader.GetOrdinal("Password")),
                                        reader.GetString(reader.GetOrdinal("Phone")),
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
                    catch(Exception ex)
                    {
                        NlogSettings.LogError(ex, "Error in GetTeacher method.");
                        throw new DataAccessException("An error occurred while retrieving teacher.", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Adds a new teacher to the database using a stored procedure.
        /// </summary>
        /// <param name="TeacherDTO">The TeacherDTO object containing the details of the teacher to add.</param>
        /// <returns>
        /// The ID of the newly added teacher if the addition is successful; 
        /// otherwise, returns -1 if an error occurs during the database operation.
        /// </returns>
        public static int AddTeacher(TeacherDTO TeacherDTO)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_AddTeacher", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Name", TeacherDTO.Name);
                        command.Parameters.AddWithValue("@Email", TeacherDTO.Email);
                        command.Parameters.AddWithValue("@Password", TeacherDTO.Password);
                        command.Parameters.AddWithValue("@Phone", TeacherDTO.Phone);
                        command.Parameters.AddWithValue("@ImagePath", TeacherDTO.ImagePath);
                        command.Parameters.AddWithValue("@Role", TeacherDTO.Role);
                        command.Parameters.AddWithValue("@DepartmentId", TeacherDTO.DepartmentId);
                        var outputIdParam = new SqlParameter("@NewTeacherId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        return (int)outputIdParam.Value;
                    }
                    catch (Exception ex)
                    {
                        NlogSettings.LogError(ex, "Error in AddTeacher method.");
                        throw new DataAccessException("An error occurred while adding teacher.", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Updates an existing teacher's details in the database using a stored procedure.
        /// </summary>
        /// <param name="TeacherDTO">The TeacherDTO object containing the updated details of the teacher.</param>
        /// <returns>
        /// A boolean value indicating whether the update was successful.
        /// Returns true if the teacher was updated successfully; otherwise, false if an error occurs.
        /// </returns>
        public static bool UpdateTeacher(TeacherDTO TeacherDTO)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_UpdateTeacher", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Id", TeacherDTO.Id);
                        command.Parameters.AddWithValue("@Name", TeacherDTO.Name);
                        command.Parameters.AddWithValue("@Email", TeacherDTO.Email);
                        command.Parameters.AddWithValue("@Password", TeacherDTO.Password);
                        command.Parameters.AddWithValue("@Phone", TeacherDTO.Phone);
                        command.Parameters.AddWithValue("@ImagePath", TeacherDTO.ImagePath);
                        command.Parameters.AddWithValue("@Role", TeacherDTO.Role);
                        command.Parameters.AddWithValue("@DepartmentId", TeacherDTO.DepartmentId);

                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch(Exception ex) {
                        NlogSettings.LogError(ex, "Error in UpdateTeacher method.");
                        throw new DataAccessException("An error occurred while updating teacher.", ex);
                    }

                }
            }
        }

        /// <summary>
        /// Deletes a teacher from the database by their ID using a stored procedure.
        /// </summary>
        /// <param name="TeacherId">The ID of the teacher to delete.</param>
        /// <returns>
        /// A boolean value indicating whether the deletion was successful.
        /// Returns true if one row was affected (deleted); otherwise, false if an error occurs or no rows were deleted.
        /// </returns>
        public static bool DeleteTeacher(int TeacherId)
        {
            int rowsAffected = 0;
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_DeleteTeacher", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TeacherId", TeacherId);

                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();
                        return (rowsAffected == 1);
                    }
                    catch(Exception ex)
                    {
                        NlogSettings.LogError(ex, "Error in DeleteTeacher method.");
                        throw new DataAccessException("An error occurred while deleting teacher.", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves a teacher's credentials from the database based on the provided email and password.
        /// </summary>
        /// <param name="email">The email address of the teacher.</param>
        /// <param name="password">The password of the teacher.</param>
        /// <returns>
        /// A <see cref="TeacherDTO"/> object containing the teacher's information if credentials are valid; otherwise, null.
        /// </returns>
        public static TeacherDTO? FindByCredentials(string email, string password)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_FindByCredentials", connection))
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
                                return new TeacherDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                        reader.GetString(reader.GetOrdinal("Name")),
                                        reader.GetString(reader.GetOrdinal("Email")),
                                        reader.GetString(reader.GetOrdinal("Password")),
                                        reader.GetString(reader.GetOrdinal("Phone")),
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
                    catch(Exception ex) 
                    {
                        NlogSettings.LogError(ex, "Error in FindByCredentials method.");
                        throw new DataAccessException("An error occurred while finding teacher.", ex);
                    }
                }
            }
        }



        /// <summary>
        /// Retrieves the name of a teacher from the database based on the specified teacher ID.
        /// </summary>
        /// <param name="TeacherId">The unique identifier of the teacher.</param>
        /// <returns>
        /// The name of the teacher as a string if found; otherwise, an empty string.
        /// </returns>
        public static string GetTeacherName(int TeacherId)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetTeacherName", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TeacherId", TeacherId);

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
                    catch(Exception ex)
                    {
                        NlogSettings.LogError(ex, "Error in GetTeacherName method.");
                        throw new DataAccessException("An error occurred while retrieving teacher name.", ex);
                    }
                }
            }
        }


        /// <summary>
        /// Retrieves the image of a teacher from the database based on the specified teacher ID.
        /// </summary>
        /// <param name="TeacherId">The unique identifier of the teacher.</param>
        /// <returns>
        /// The image of the teacher as a string if found; otherwise, an empty string.
        /// </returns>
        public static string? GetTeacherImage(int id)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_GetTeacherImage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TeacherId", id);

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
                        NlogSettings.LogError(ex, "Error in GetTeacherImage method.");
                        throw new DataAccessException("An error occurred while retrieving the student image.", ex);
                    }
                }
            }
        }

        public static TeacherDTO? Login(string email, string password)
        {
            using (var connection = new SqlConnection(DataAccessSettings.ConnectionString))
            {
                using (var command = new SqlCommand("SP_TeacherLogin", connection))
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
                                return new TeacherDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                        reader.GetString(reader.GetOrdinal("Name")),
                                        reader.GetString(reader.GetOrdinal("Email")),
                                        reader.GetString(reader.GetOrdinal("Password")),
                                        reader.GetString(reader.GetOrdinal("Phone")),
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
