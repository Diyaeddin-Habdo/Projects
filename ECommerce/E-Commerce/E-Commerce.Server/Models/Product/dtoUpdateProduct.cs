namespace E_Commerce.Server.Models.Product
{
    public class dtoUpdateProduct
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public decimal Price { get; set; }

        public decimal Discount { get; set; }
        public string About { get; set; }

        public List<IFormFile> Images { get; set; }
    }
}
