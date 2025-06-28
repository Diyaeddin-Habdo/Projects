using Business_layer;
using E_Commerce.Server.Models.Category;
using E_Commerce.Server.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Server.Controllers
{
    [Route("api/Categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet(Name = "GetAllCategories")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<DataAccess_layer.Models.dtoCategory>> GetAllCategories()
        {
            List<DataAccess_layer.Models.dtoCategory> result = clsCategory.GetAllCategories();
            if (result.Count == 0 || result == null)
            {
                return NotFound("No category Found!");
            }
            return Ok(result); // Returns the list of students.
        }


        [HttpGet("Page/{PageNumber}/{PageSize}", Name = "GetCategoriesPage")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<DataAccess_layer.Models.dtoCategory>> GetCategoriesPage(int PageNumber, int PageSize)
        {
            if (PageNumber <= 0 || PageSize <= 0)
                return BadRequest("Invalid data");

            var categories = clsCategory.GetCategoriesPage(PageNumber, PageSize);

            if (categories.Count == 0)
                return NotFound("No products found!");

            return Ok(categories);
        }


        [HttpGet("{id}", Name = "GetCategoryById")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<DataAccess_layer.Models.dtoCategory> GetCategoryById(int id)
        {

            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            var result = clsCategory.Find(id);
            DataAccess_layer.Models.dtoCategory category = new DataAccess_layer.Models.dtoCategory(-1,"","");
            if(result != null)
            {
                category.id = result.id;
                category.name = result.name;
                category.name = result.name;
            }

            if (result == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }


            return Ok(category);

        }



        [HttpPost(Name = "AddCategory")]
        [Authorize(Roles = "Admin,Product Manager")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DataAccess_layer.Models.dtoCategory>> AddCategory(dtoAddCategory newCategory)
        {

            if (newCategory == null || string.IsNullOrEmpty(newCategory.name)
                || newCategory.image == null)
            {
                return BadRequest("Invalid category data.");
            }

            var imageUrl = await CloudinaryService.UploadImageAsync(newCategory.image);
            if(imageUrl == null || string.IsNullOrEmpty(imageUrl.Trim()))
                return StatusCode(500, "An error accorded while adding the image.");

            clsCategory category = new clsCategory();
            category.name = newCategory.name;
            category.image = imageUrl;

            if (category.Save())
                return CreatedAtRoute("GetCategoryById", new { id = category.id }, new DataAccess_layer.Models.dtoCategory(category.id, category.name, category.image));
            else
                return StatusCode(500, "An error accorded while adding the user.");
        }




        [HttpPut("{id}", Name = "UpdateCategory")]
        [Authorize(Roles = "Admin,Product Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCategory(int id,[FromForm] dtoUpdateCategory updated)
        {
            if (id < 1 || updated == null || string.IsNullOrEmpty(updated.name)
                || updated.image == null)
            {
                return BadRequest("Invalid category data.");
            }

            clsCategory result = clsCategory.Find(id);
            if (result == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            var newUrl = await CloudinaryService.ReplaceImageAsync(updated.image, result.image);

            result.name = updated.name;
            result.image = newUrl;

            if (result.Save())
                return Ok(new { result.id, result.name, result.image});
            else
                return StatusCode(500, "An error accorded while updating the category.");
        }



        //here we use HttpDelete method
        [HttpDelete("{id}", Name = "DeleteCategory")]
        [Authorize(Roles = "Admin,Product Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            clsCategory category = clsCategory.Find(id);

            if (category == null)
                return NotFound($"Category with ID {id} not found. no rows deleted!");


            await CloudinaryService.DeleteImageAsync(category.image);

            if (clsCategory.DeleteCategory(id))
                return Ok($"Category with ID {id} has been deleted.");
            else
                return StatusCode(500, "An error accorded while deleting the category.");
        }
    }
}
