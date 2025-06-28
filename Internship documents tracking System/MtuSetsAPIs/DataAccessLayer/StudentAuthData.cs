using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class StudentAuthData
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
        
        public string Token { get; set; }

        public StudentAuthData(int id, string name, string email, string phone, string imagePath, string SNo, string token, string role,string departmentId)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Phone = phone;
            this.ImagePath = imagePath;
            this.SNo = SNo;
            this.Token = token;
            this.Role = role;
            this.DepartmentId = departmentId;
         }
    }
}
