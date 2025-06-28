using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Student
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public StudentDTO SDTO
        {
            get
            {
                return (new StudentDTO(this.Id, this.Name, this.Email, this.Password, this.Phone,this.SNo, this.ImagePath,this.Role,this.DepartmentId));
            }
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string SNo { get; set; }
        public string ImagePath { get; set; }
        public string Role { get; set; }
        public string DepartmentId { get; set; }

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
        public Student(StudentDTO SDTO, enMode cMode = enMode.AddNew)
        {
            this.Id = SDTO.Id;
            this.Name = SDTO.Name;
            this.Email = SDTO.Email;
            this.Password = ComputeHash(SDTO.Password);
            this.Phone = SDTO.Phone;
            this.SNo = SDTO.SNo;
            this.ImagePath = SDTO.ImagePath;
            this.Role = SDTO.Role;
            this.DepartmentId = SDTO.DepartmentId;
            
            Mode = cMode;
        }

        /// <summary>
        /// Adds a new student using the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the addition was successful.
        /// Returns true if the student was added successfully; otherwise, false.
        /// </returns>
        private bool _AddNewStudent()
        {
            try {
                this.Id = StudentData.AddStudent(SDTO);
                return (this.Id != -1);
            }
            catch{
                throw;
            }
        }

        /// <summary>
        /// Updates an existing student using the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the update was successful.
        /// Returns true if the student was updated successfully; otherwise, false.
        /// </returns>
        private bool _UpdateStudent()
        {
            try {
                return StudentData.UpdateStudent(SDTO);
            }
            catch { 
                throw;
            }
        }

        /// <summary>
        /// Retrieves a list of all students from the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A list of StudentDTO objects representing all students in the system.
        /// </returns>
        public static List<StudentDTO> GetAllStudent()
        {
            try {
                return StudentData.GetAllStudents();
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Finds a student by their ID in the Data Access Layer.
        /// </summary>
        /// <param name="ID">The ID of the student to find.</param>
        /// <returns>
        /// A student object if found; otherwise, null.
        /// </returns>
        public static Student? Find(int ID)
        {
            try {
                var SDTO = StudentData.GetStudentById(ID);

                if (SDTO != null)
                //we return new object of that student with the right data
                {

                    return new Student(SDTO, enMode.Update);
                }
                else
                    return null;
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Saves the current student object to the Data Access Layer.
        /// </summary>
        /// <returns>
        /// A boolean value indicating whether the save operation was successful.
        /// Returns true if the student was added or updated successfully; otherwise, false.
        /// </returns>
        public bool Save()
        {
            try {
                switch (Mode)
                {
                    case enMode.AddNew:
                        if (_AddNewStudent())
                        {

                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case enMode.Update:

                        return _UpdateStudent();

                }
            }
            catch {
                throw;
            }

            return false;
        }

        /// <summary>
        /// Deletes a student from the Data Access Layer by their ID.
        /// </summary>
        /// <param name="ID">The ID of the student to delete.</param>
        /// <returns>
        /// A boolean value indicating whether the deletion was successful.
        /// Returns true if the student was deleted successfully; otherwise, false.
        /// </returns>
        public static bool DeleteStudent(int ID)
        {
            try {
                return StudentData.DeleteStudent(ID);
            }
            catch {
                throw;
            }
        }

        //public static int Register(string name, string email, string password,string phone,string sno,string image)
        //{
        //    var id = StudentData.AddStudent(new StudentDTO(0,name,email,ComputeHash(password),phone,sno,image,"Student"));
        //    return id;
        //}

        ///// <summary>
        ///// Finds a student by their email and password credentials.
        ///// </summary>
        ///// <param name="email">The email of the student.</param>
        ///// <param name="password">The password of the student.</param>
        ///// <returns>
        ///// A student object if the credentials are valid; otherwise, returns null.
        ///// </returns>
        //public static Student? FindByCredentials(string email, string password)
        //{
        //    try {
        //        var user = StudentData.FindByCredentials(email, ComputeHash(password));

        //        if (user != null)
        //            return new Student(user, enMode.Update);
        //        else
        //            return null;
        //    }
        //    catch {
        //        throw;
        //    }
        //}

        // Private Field
        private static IConfiguration _config;

        /// <summary>
        /// Authenticates a Student by their email and password and generates an authentication token.
        /// </summary>
        /// <param name="email">The email of the Student.</param>
        /// <param name="password">The password of the Student.</param>
        /// <param name="config">The configuration settings used to generate the token.</param>
        /// <returns>
        /// An AuthData object containing the Student's details and authentication token if the login is successful; otherwise, returns null.
        /// </returns>
        public static StudentAuthData? Login(string email, string password, IConfiguration config)
        {
            try {
                _config = config;
                var user = StudentData.Login(email, ComputeHash(password));
                if (user != null)
                    return new StudentAuthData(user.Id, user.Name, user.Email, user.Phone, user.ImagePath, user.SNo, GenerateToken(user), user.Role,user.DepartmentId);
                else
                    return null;
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Generates a JSON Web Token (JWT) for a given Student user.
        /// </summary>
        /// <param name="user">The Student user for whom the token is generated.</param>
        /// <returns>
        /// A string representing the generated JWT, which can be used for authenticating the user.
        /// </returns>
        public static string GenerateToken(StudentDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim (ClaimTypes.NameIdentifier,user.Name),
                new Claim (ClaimTypes.Email,user.Email),
                new Claim (ClaimTypes.MobilePhone,user.Phone),
                new Claim("SNo",user.SNo),
                new Claim(ClaimTypes.Role,user.Role),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Retrieves a list of students with pending status.
        /// </summary>
        /// <returns>A list of pending students.</returns>
        public static List<StudentDTO> GetPendingStudents(string DepartmentId)
        {
            try {
                return StudentData.GetPendingStudents(DepartmentId);
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a list of students approved from teacher.
        /// </summary>
        /// <returns>A list of students approved from teacher.</returns>
        public static List<StudentDTO> GetTeacherApprovalStudents(string DepartmentId)
        {
            try
            {
                return StudentData.GetTeacherApprovalStudents(DepartmentId);
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a list of students approved from advisor.
        /// </summary>
        /// <returns>A list of students approved from advisor.</returns>
        public static List<StudentDTO> GetAdvisorApprovalStudents(string DepartmentId)
        {
            try
            {
                return StudentData.GetAdvisorApprovalStudents(DepartmentId);
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a list of students with unapproved status by the teacher.
        /// </summary>
        /// <returns>A list of unapproved students by the teacher.</returns>
        public static List<StudentDTO> GetTeacherUnapprovalStudents(string DepartmentId)
        {
            try {
                return StudentData.GetTeacherUnapprovalStudents(DepartmentId);
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a list of students with unapproved status by the advisor.
        /// </summary>
        /// <returns>A list of unapproved students by the advisor.</returns>
        public static List<StudentDTO> GetAdvisorUnapprovalStudents(string DepartmentId)
        {
            try
            {
                return StudentData.GetAdvisorUnapprovalStudents(DepartmentId);
            }
            catch {
                throw;
            }
        }


        /// <summary>
        /// Retrieves the name of a student by their ID.
        /// </summary>
        /// <param name="id">The ID of the student.</param>
        /// <returns>The name of the student, or empty string if not found.</returns>
        public static string GetStudentName(int id)
        {
            try
            {
                return StudentData.GetStudentName(id);
            }
            catch {
                throw;
            }
        }



        public static string? GetStudentImage(int id)
        {
            return StudentData.GetStudentImage(id);
        }
    }
}
