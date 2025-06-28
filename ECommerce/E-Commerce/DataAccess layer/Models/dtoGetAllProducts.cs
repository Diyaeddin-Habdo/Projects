using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_layer.Models
{
    public class dtoGetAllProducts
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public decimal Price { get; set; }

        public decimal Discount { get; set; }
        public string About { get; set; }

        public List<string> Images { get; set; }
    }
}
