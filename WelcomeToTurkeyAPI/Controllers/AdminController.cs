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
            var categories = dbContext.Categories.Select(x => new ListOfCategories
            {
                CategoryId = x.Id,
                CategoryName = x.CategoryName
            }).ToList();

            if (categories != null)
            {
                return Ok(categories);
            }
            return BadRequest();
        }

        // List Of Blogs

        [HttpGet("list-blog-with-Id")]
        public IActionResult ListBlogs()
        {
            var blogs = dbContext.Blogs.Select(x => new ListAllBlogsDto
            {
                BlogId = x.Id,
                Title = x.Title,
                Category = x.Category.CategoryName,
                Content = x.Content,
                PublishDate = x.PublishDate,
                IsPublished = x.IsPublished

            }).ToList();

            if (blogs != null)
            {
                return Ok(blogs);
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

        [HttpPost("add-new-blog")]

        public IActionResult AddNewBlog([FromBody] AddNewBlogDto newBlog)
        {
            var blog = new Blog()
            {
                Title = newBlog.Title,
                CategoryId = newBlog.CategoryId,
                IsPublished = false
            };

            if (blog is not null)
            {
                dbContext.Add(blog);
                var result = dbContext.SaveChanges();
                if (result > 0) { return Ok("İşlem Başarılı..."); }
                return BadRequest();
            }
            return Ok("İşlem Başarısız!!!");

        }

        /*DELETE TRANSACTIONS*/

        // Delete Categories
        [HttpDelete("delete-category-by-Id/{categoryId}")]
        public IActionResult DeleteCategoryById([FromRoute] int categoryId)
        {
            var categoryById = dbContext.Categories.SingleOrDefault(c => c.Id == categoryId);
            if (categoryById != null)
            {
                dbContext.Categories.Remove(categoryById);
                var result = dbContext.SaveChanges();
                if (result > 0)
                {
                    return Ok();
                }
                return BadRequest();

            }
            return BadRequest();

        }


    }
}
