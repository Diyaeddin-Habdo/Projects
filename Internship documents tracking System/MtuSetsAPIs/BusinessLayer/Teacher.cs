using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using System.Net.Http;
using System.Linq.Expressions;


namespace BusinessLayer
{
    public class Teacher
    {
        // Enum for operation mode (Adding or Updating).
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        // Private Field
        private static IConfiguration _config;

        // Property
        public TeacherDTO TDTO
        {
            get
            {
                return (new TeacherDTO(this.Id,this.Name,this.Email,this.Password,this.Phone,this.ImagePath,this.Role,this.DepartmentId));
            }
        }

        // Fields
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public string ImagePath { get; set; }

        public string Role { get; set; }
        public string DepartmentId { get; set; }

        // Constructure
        public Teacher(DataAccessLayer.TeacherDTO TDTO, enMode cMode = enMode.AddNew)
        {
            this.Id = TDTO.Id;
            this.Name = TDTO.Name;
            this.Email = TDTO.Email;
            this.Password = ComputeHash(TDTO.Password);
            this.Phone = TDTO.Phone;
            this.ImagePath = TDTO.ImagePath;
            this.Role = TDTO.Role;
            this.DepartmentId = TDTO.DepartmentId;

            Mode = cMode;
        }

        // To hashing the password.
        public static string ComputeHash(string input)
        {
            //SHA is Secutred Hash Algorithm.
            // Create an instance of the SHA-256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }


        /// <summary>
        /// Adds a new teacher using the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the addition was successful.
        /// Returns true if the teacher was added successfully; otherwise, false.
        /// </returns>
        private bool _AddNewTeacher()
        {
            try
            {
                this.Id = TeacherData.AddTeacher(TDTO);
                return (this.Id != -1);
            }
            catch(DataAccessException)
            {
                throw;
            }
        }


        /// <summary>
        /// Updates an existing teacher using the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the update was successful.
        /// Returns true if the teacher was updated successfully; otherwise, false.
        /// </returns>
        private bool _UpdateTeacher()
        {
            try
            {
                return TeacherData.UpdateTeacher(TDTO);
            }
            catch (DataAccessException) {
                throw;
            }
        }


        /// <summary>
        /// Retrieves a list of all teachers from the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A list of TeacherDTO objects representing all teachers in the system.
        /// </returns>
        public static List<TeacherDTO> GetAllTeacher()
        {
            try
            {
                return TeacherData.GetAllTeachers();
            }
            catch(DataAccessException)
            {
                throw;
            }
        }



        /// <summary>
        /// Finds a teacher by their ID in the Data Access Layer.
        /// </summary>
        /// <param name="ID">The ID of the teacher to find.</param>
        /// <returns>
        /// A Teacher object if found; otherwise, null.
        /// </returns>

        public static Teacher? Find(int ID)
        {
            try
            {
                var result = DataAccessLayer.TeacherData.GetTeacherById(ID);

                if (result != null)
                //we return new object of that student with the right data
                {

                    return new Teacher(result, enMode.Update);
                }
                else
                    return null;
            }
            catch(DataAccessException)
            {
                throw;
            }
        }


        /// <summary>
        /// Saves the current teacher object to the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the save operation was successful.
        /// Returns true if the teacher was added or updated successfully; otherwise, false.
        /// </returns>
        public bool Save()
        {
            try {
                switch (Mode)
                {
                    case enMode.AddNew:
                        if (_AddNewTeacher())
                        {

                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case enMode.Update:

                        return _UpdateTeacher();

                }
            }
            catch (DataAccessException) {
                throw;
            }
            return false;
        }


        /// <summary>
        /// Deletes a teacher from the Data Access Layer by their ID.
        /// </summary>
        /// <param name="ID">The ID of the teacher to delete.</param>
        /// <returns>
        /// A boolean value indicating whether the deletion was successful.
        /// Returns true if the teacher was deleted successfully; otherwise, false.
        /// </returns>
        public static bool DeleteTeacher(int ID)
        {
            try
            {
                return TeacherData.DeleteTeacher(ID);
            }
            catch (DataAccessException)
            {
                throw;
            }
        }


        ///// <summary>
        ///// Finds a teacher by their email and password credentials.
        ///// </summary>
        ///// <param name="email">The email of the teacher.</param>
        ///// <param name="password">The password of the teacher.</param>
        ///// <returns>
        ///// A Teacher object if the credentials are valid; otherwise, returns null.
        ///// </returns>
        //public static Teacher? FindByCredentials(string email, string password)
        //{
        //    try
        //    {
        //        var user = TeacherData.FindByCredentials(email, ComputeHash(password));

        //        if (user != null)
        //            return new Teacher(user, enMode.Update);
        //        else
        //            return null;
        //    }
        //    catch (DataAccessException)
        //    {
        //        throw;
        //    }
        //}


        /// <summary>
        /// Authenticates a teacher by their email and password and generates an authentication token.
        /// </summary>
        /// <param name="email">The email of the teacher.</param>
        /// <param name="password">The password of the teacher.</param>
        /// <param name="config">The configuration settings used to generate the token.</param>
        /// <returns>
        /// An AuthData object containing the teacher's details and authentication token if the login is successful; otherwise, returns null.
        /// </returns>
        public static AuthData? Login(string email, string password, IConfiguration config)
        {
            try
            {
                _config = config;
                var user = TeacherData.Login(email, ComputeHash(password));
                if (user != null)
                    return new AuthData(user.Id, user.Name, user.Email, user.Phone, user.ImagePath, user.Role, GenerateToken(user),user.DepartmentId);
                else
                    return null;
            }
            catch (DataAccessException) {
                throw;
            }
        }

        /// <summary>
        /// Generates a JSON Web Token (JWT) for a given teacher user.
        /// </summary>
        /// <param name="user">The teacher user for whom the token is generated.</param>
        /// <returns>
        /// A string representing the generated JWT, which can be used for authenticating the user.
        /// </returns>
        public static string GenerateToken(TeacherDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim (ClaimTypes.NameIdentifier,user.Name),
                new Claim (ClaimTypes.Email,user.Email),
                new Claim (ClaimTypes.Role,user.Role),
                new Claim (ClaimTypes.MobilePhone,user.Phone)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Retrieves the name of a teacher by their ID.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <returns>The name of the teacher, or empty string if not found.</returns>
        public static string GetTeacherName(int id)
        {
            try
            {
                return DataAccessLayer.TeacherData.GetTeacherName(id);
            }
            catch (DataAccessException) {
                throw;
            }
        }

        public static string? GetTeacherImage(int id)
        {
            return TeacherData.GetTeacherImage(id);
        }
    }
}
