using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_layer.Models
{
    public class dtoCategory
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }

        public dtoCategory(int id, string name, string image)
        {
            this.id = id;
            this.name = name;
            this.image = image;
        }
    }
}
