using DataAccess_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using DataAccess_layer.Models;


namespace Business_layer
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string roles { get; set; }

        private static IConfiguration _config;
        public clsUser() 
        { 
            this.id = -1;
            this.name = string.Empty;
            this.email = string.Empty;
            this.password = string.Empty;
            this.roles = string.Empty;

            this.Mode = enMode.AddNew;
        }
        private clsUser(int id,string name,string email,string password,string roles) { 
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
            this.roles = roles;

            this.Mode = enMode.Update;
        }

        private static string ComputeHash(string input)
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
        private bool _AddNewUser()
        {
            this.id = clsUserData.AddUser(name,email,ComputeHash(password), roles);
            return (this.id != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(id,name,email, roles);
        }

        public static List<DataAccess_layer.Models.dtoUser> GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static dtoUser Find(int ID)
        {
            string name = "", email = "", roles = "";

            bool isFound = clsUserData.Find(ID,ref name,ref email,ref roles);

            if (isFound)
            {
                return new dtoUser(ID,name,email, roles);
            }
            else
                return null;
        }

        public static clsUser FindById(int ID)
        {
            string name = "", email = "",password = "", roles = "";

            bool isFound = clsUserData.FindById(ID, ref name, ref email,ref password, ref roles);

            if (isFound)
            {
                return new clsUser(ID, name, email,password, roles);
            }
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }

        public static bool DeleteUser(int ID)
        {
            return clsUserData.Delete(ID);
        }

        public static bool IsExists(int ID)
        {
            return clsUserData.IsExists(ID);
        }

        public static clsUser FindByCredentials(string email, string password)
        {
            int id = -1;
            string name = "", roles = "";

            bool isFound = clsUserData.FindByCredentials(email,ComputeHash(password),ref name,ref id,ref roles);

            if (isFound)
                return new clsUser(id, name, email, password, roles);
            else
                return null;
        }
        public static dtoAuthenticatedUser Register(string name,string email,string password,string roles,IConfiguration configuration)
        {
            _config = configuration;

            clsUser user = new clsUser();
            user.name = name;
            user.email = email;
            user.password = password;
            user.roles = roles;

            if(user.Save())
            {

                return new dtoAuthenticatedUser(user.id,user.name,user.email,user.roles,GenerateToken(user));
            }
            else
            {
                return null;
            }
        }

        public static dtoAuthenticatedUser Login(string email,string password,IConfiguration config)
        {
            _config = config;
            var user = clsUser.FindByCredentials(email, password);
            if (user != null)
                return new dtoAuthenticatedUser(user.id, user.name, user.email, user.roles, GenerateToken(user));
            else
                return null;
        }
        public static string GenerateToken(clsUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim (ClaimTypes.NameIdentifier,user.name),
                new Claim (ClaimTypes.Email,user.email),
                new Claim (ClaimTypes.Role,user.roles)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
