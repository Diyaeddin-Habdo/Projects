using Business_layer;
using E_Commerce.Server.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_Commerce.Server.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet(Name = "GetAllProducts")] // Marks this method to respond to HTTP GET requests.
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DataAccess_layer.Models.dtoGetAllProducts>> GetAll()
        {
            var products = clsProduct.GetAll();

            if (products.Count == 0)
                return NotFound("No products found!");

            return Ok(products);
        }


        [HttpGet("Page/{PageNumber}/{PageSize}",Name = "GetProductsPage")] // Marks this method to respond to HTTP GET requests.
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DataAccess_layer.Models.dtoGetAllProducts>> GetProductsPage(int PageNumber,int PageSize)
        {
            if (PageNumber <= 0 || PageSize <= 0)
                return BadRequest("Invalid data");

            var products = clsProduct.GetProductsPage(PageNumber,PageSize);

            if (products.Count == 0)
                return NotFound("No products found!");

            return Ok(products);
        }


        [HttpGet("LatestSales/{count}",Name = "GetLatest_N_Sales")] // Marks this method to respond to HTTP GET requests.
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DataAccess_layer.Models.dtoGetAllProducts>> GetLatest_N_Sales(int count)
        {
            var products = clsProduct.GetLatest_N_Sales(count);

            if (products.Count == 0)
                return NotFound("No products found!");

            return Ok(products);
        }



        [HttpGet("LatestProducts/{count}", Name = "GetLatestProducts")] // Marks this method to respond to HTTP GET requests.
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DataAccess_layer.Models.dtoGetAllProducts>> LatestProducts(int count)
        {
            var products = clsProduct.LatestProducts(count);

            if (products.Count == 0)
                return NotFound("No products found!");

            return Ok(products);
        }



        [HttpGet("TopRated/{count}", Name = "Get_N_TopRated")] // Marks this method to respond to HTTP GET requests.
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DataAccess_layer.Models.dtoGetAllProducts>> Get_N_TopRated(int count)
        {
            var products = clsProduct.Get_N_TopRated(count);

            if (products.Count == 0)
                return NotFound("No products found!");

            return Ok(products);
        }



        [HttpGet("{id}", Name = "GetProduct")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<dtoGetProduct> Get(int id)
        {
            if (id < 1)
                return BadRequest($"Not accepted ID {id}");

            clsProduct product = clsProduct.Find(id);
            if (product != null)
            {
                dtoGetProduct p = new dtoGetProduct()
                {
                    Id = product.Id,
                    About = product.About,
                    CategoryId = product.CategoryId,
                    Rating = product.Rating,
                    Description = product.Description,
                    Price = product.Price,
                    Discount = product.Discount,
                    Images = product.Images,
                    Title = product.Title
                };
                return Ok(p);
            }


            return NotFound($"Product with ID {id} not found!");
        }



        [HttpPost(Name = "AddProduct")]
        [Authorize(Roles = "Admin,Product Manager")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<dtoAddProduct>> Add([FromForm] dtoAddProduct product)
        {
            if (product == null || product.CategoryId < 1 || product.Rating < 0 || product.Price < 0 || product.Discount < 0
                || string.IsNullOrEmpty(product.Title) || string.IsNullOrEmpty(product.Description) || string.IsNullOrEmpty(product.About)
                || product.Images.Count == 0)
                return BadRequest("Invalid product data.");


            var images = CloudinaryService.UploadImagesAsync(product.Images);

            clsProduct p = new clsProduct();
            p.CategoryId = product.CategoryId;
            p.Title = product.Title;
            p.Description = product.Description;
            p.Rating = product.Rating;
            p.Price = product.Price;
            p.Discount = product.Discount;
            p.About = product.About;
            p.Images = await images;

            if (p.Save())
            {
                dtoGetProduct pro = new dtoGetProduct()
                {
                    Id = p.Id,
                    About = p.About,
                    CategoryId = p.CategoryId,
                    Rating = p.Rating,
                    Description = p.Description,
                    Price = p.Price,
                    Discount = p.Discount,
                    Images = p.Images,
                    Title = p.Title
                };
                return CreatedAtRoute("GetProduct", new { id = p.Id }, pro);
            }
            return StatusCode(500, "An error occurred while saving the product.");
        }


        [HttpPut(Name = "UpdateProduct")]
        [Authorize(Roles = "Admin,Product Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<dtoAddProduct>> Update(dtoUpdateProduct product)
        {
            if (product == null || product.CategoryId < 1 || product.Rating < 0 || product.Price < 0 || product.Discount < 0
                || string.IsNullOrEmpty(product.Title) || string.IsNullOrEmpty(product.Description) || string.IsNullOrEmpty(product.About)
                || product.Images.Count == 0)
                return BadRequest("Invalid product data.");

            clsProduct p = clsProduct.Find(product.Id);

            if (p == null)
                return NotFound($"Product with ID {product.Id} not found!");

            var images = CloudinaryService.ReplaceImagesAsync(p.Images,product.Images);


            p.CategoryId = product.CategoryId;
            p.Title = product.Title;
            p.Description = product.Description;
            p.Rating = product.Rating;
            p.Price = product.Price;
            p.Discount = product.Discount;
            p.About = product.About;
            p.Images = await images;

            if (p.Save())
            {
                dtoGetProduct pro = new dtoGetProduct()
                {
                    Id = p.Id,
                    About = p.About,
                    CategoryId = p.CategoryId,
                    Rating = p.Rating,
                    Description = p.Description,
                    Price = p.Price,
                    Discount = p.Discount,
                    Images = p.Images,
                    Title = p.Title
                };
                return CreatedAtRoute("GetProduct", new { id = p.Id }, pro);
            }
            return StatusCode(500, "An error occurred while saving the product.");
        }



        [HttpDelete("{id}", Name = "DeleteProduct")]
        [Authorize(Roles = "Admin,Product Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            if (id < 1)
                return BadRequest($"Not accepted ID {id}");

            var product = clsProduct.Find(id);
            if(product == null)
                return NotFound("No product found");

            await CloudinaryService.DeleteImagesAsync(product.Images.ToArray());
            if (clsProduct.Delete(id))
            {
                return Ok();
            }
            else
                return NotFound();
        }



    }
}
