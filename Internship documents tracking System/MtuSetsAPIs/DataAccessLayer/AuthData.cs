using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class AuthData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImagePath { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string DepartmentId { get; set; }

        public AuthData(int id, string name, string email, string phone, string imagePath, string role,string token, string departmentId)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Phone = phone;
            this.ImagePath = imagePath;
            this.Role = role;
            this.Token = token;
            this.DepartmentId = departmentId;
        }

    }
}
