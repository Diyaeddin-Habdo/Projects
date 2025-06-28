using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_layer.Models
{
    public class dtoUser
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string roles { get; set; }

        public dtoUser(int id,string name,string email,string roles) 
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.roles = roles;
        }
    }
}
