using Business_layer;

namespace E_Commerce.Server.Models.Product
{
    public class dtoGetProduct
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

        public dtoGetProduct()
        {
            this.Id = 0;
            this.CategoryId = 0;
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.Rating = 0;
            this.Price = 0;
            this.Discount = 0;
            this.About = string.Empty;
            this.Images = new List<string>();
        }

        public dtoGetProduct(int id, int categoryId,string title,string desc,int rating,decimal price,decimal discount,string about
            ,List<string> images)
        {
            Id = id;
            CategoryId = categoryId;
            Title = title;
            Description = desc;
            Rating = rating;
            Price = price;
            Discount = discount;
            About = about;
            Images = images;
        }
    }
}
