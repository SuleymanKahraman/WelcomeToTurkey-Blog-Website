using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WelcomeToTurkeyAPI.Data.Entities;
using WelcomeToTurkeyAPI.Dtos.BlogDtos;
using WelcomeToTurkeyAPI.Dtos.CategoryDtos;

namespace WelcomeToTurkeyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly WTTDbContext dbContext;

        public AdminController(WTTDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /* LIST TRANSACTIONS */

        // List Of Categories

        [HttpGet("list-category-with-Id")]

        public IActionResult ListCategory()
        {
            var categories = dbContext.Categories.Select(x=> new ListOfCategories
            {
                CategoryId = x.Id,
                CategoryName  =x.CategoryName
            }).ToList();

            if(categories !=null)
            {
                return Ok(categories);
            }
            return BadRequest();
        }
            

        // ADD CATEGORY

        [HttpPost("add-category/{categoryName}")]

        public IActionResult AddCategory([FromRoute] string categoryName)
        {
            var currentCategories = dbContext.Categories.SingleOrDefault(x => x.CategoryName == categoryName);
            if (currentCategories == null)
            {
                var newCategory = new Category()
                {
                    CategoryName = categoryName,
                };

                if (newCategory is not null)
                {
                    dbContext.Categories.Add(newCategory);
                    dbContext.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            return BadRequest();

        }

        [HttpPost("add-blog")]

        public IActionResult AddNewBlog([FromBody] AddNewBlogDto blog)
        {
            //var newBlog = new Blog()
            //{
            //    Title = blog.Title,


            //}
            return Ok();
        }

        /*DELETE TRANSACTIONS*/

        // Delete Categories
        [HttpDelete("delete-category-by-Id/{categoryId}")]
        public IActionResult DeleteCategoryById([FromRoute] int id)
        {
            return Ok();
        }

    }
}
