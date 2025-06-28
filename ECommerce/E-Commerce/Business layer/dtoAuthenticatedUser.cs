using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_layer
{
    public class dtoAuthenticatedUser
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string roles { get; set; }
        public string token { get; set; }

        public dtoAuthenticatedUser(int id, string name,  string email, string roles, string token)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.roles = roles;
            this.token = token;
        }
    }
}
