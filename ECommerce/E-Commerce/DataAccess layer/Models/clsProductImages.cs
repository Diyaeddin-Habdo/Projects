using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_layer.Models
{
    public class clsProductImages
    {
        public int id {  get; set; }
        public string image { get; set; }
        public int productId { get; set; }

        public clsProductImages(int id,string image,int productId) 
        {
            this.id = id;
            this.image = image;
            this.productId = productId;
        }

    }
}
