using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WelcomeToTurkeyAPI.Data.Entities;
using WelcomeToTurkeyAPI.Dtos.BlogDtos;
using WelcomeToTurkeyAPI.Dtos.CategoryDtos;
using WelcomeToTurkeyAPI.Dtos.UsersDtos;

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

        // List Of Users

        [HttpPost("list-all-users")]
        public IActionResult GetUsersByFilter([FromBody] GetUserByFiltered filter)
        {
            var users = dbContext.Users.Select(x => new ListOfUsersByFilter
            {
                UserId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                EmailAdress = x.EmailAdress,
                UserType = x.UserType,
            }).ToList();

            if(filter.FilterChars != null)
            {
                users = users.Where(u => u.LastName.StartsWith(filter.FilterChars)).ToList();
            }
            if(filter.UserType == Data.Enums.UserTypes.Admin)
            {
               users= users.Where(u => u.UserType == Data.Enums.UserTypes.Admin).ToList();
            }

            return Ok(users);

        }

        // List Of Categories

        [HttpGet("list-category-with-Id")]

        public IActionResult ListCategory()
        {
            var categories = dbContext.Categories.Select(x => new OptionCategory
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
                PublishDate = x.PublishDate.ToShortDateString(),
                IsPublished = x.IsPublished

            }).ToList();

            if (blogs != null)
            {
                return Ok(blogs);
            }
            return BadRequest();
        }

        // List Of Blog By Id

        [HttpGet("get-blog-by-Id/{blogId}")]
        public IActionResult GetBlogById([FromRoute] int blogId)
        {
            var blogById = dbContext.Blogs.Where(b => b.Id == blogId).Select(b => new GetBlogByIdDto
            {
                Title = b.Title,
                Category = b.Category.CategoryName,
                Content = b.Content,
                PublishDate = b.PublishDate.ToShortDateString(),
                IsPublished = b.IsPublished,
                Photo = b.Photo != null ? Convert.ToBase64String(b.Photo) : null
            }).SingleOrDefault();

            if (blogById != null)
            {
                return Ok(blogById);
            }
            return BadRequest();
        }

        // List Blog By Filter

        [HttpPost("list-blog-by-filter/{categoryId}")]

        public IActionResult ListByFilter([FromRoute] int categoryId)
        {
            var filteredData = dbContext.Blogs.Where(b => b.CategoryId == categoryId).Select(x => new ListAllBlogsDto
            {
                BlogId = x.Id,
                Title = x.Title,
                Category = x.Category.CategoryName,
                Content = x.Content,
                PublishDate = x.PublishDate.ToShortDateString(),
                IsPublished = x.IsPublished,
            }).ToList();
            if (filteredData != null) { return Ok(filteredData); }
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
        /* UPDATE TRANSACTIONS */

        //Update Blog By Id

        [HttpPut("update-blog-by-Id")]
        public IActionResult UpdateBlogById([FromForm]  UpdateBlogByIdDto update)
        {
            byte[] photo;
            var currentBlog = dbContext.Blogs.SingleOrDefault(b => b.Id == update.BlogId);
            if (currentBlog is not null)
            {
                currentBlog.Title = update.Title != null ? update.Title : currentBlog.Title;
                currentBlog.Content = update.Content != null ? update.Content : currentBlog.Content;
                currentBlog.IsPublished = update.IsPublished;
                if (update.IsPublished)
                {
                    currentBlog.PublishDate = DateTime.Now;
                }
                currentBlog.CategoryId = update.CategoryId != 0 ? update.CategoryId : currentBlog.CategoryId;
                if (update.Photo != null)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        update.Photo.CopyTo(stream);
                        photo = stream.ToArray();
                    }
                    currentBlog.Photo = photo;
                }
                var result = dbContext.SaveChanges();
                if (result > 0) { return Ok("Güncelleme İşlemi Başarılı..."); }
                return Ok("Değişiklik Yapmadınız!!!");

            }
            return Ok();
        }

        // Update Category By Id

        [HttpPut("update-category-by-Id")]
        public IActionResult UpdateCategoryById([FromBody] OptionCategory opt)
        {
            var currentCategroy = dbContext.Categories.SingleOrDefault(c => c.Id == opt.CategoryId);
            if (currentCategroy is not null)
            {
                currentCategroy.CategoryName = opt.CategoryName;
                var result = dbContext.SaveChanges();
                if (result > 0)
                {
                    return Ok("Güncelleme İşlemi Başarılı...");
                }
                return BadRequest();

            }
            return Ok("Category Bulunamamıştır!!!");
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

        // Delete Blog By Id

        [HttpDelete("delete-blog-by-Id/{blogId}")]
        public IActionResult DeleteBlogById([FromRoute] int blogId)
        {
            var blog = dbContext.Blogs.SingleOrDefault(b => b.Id == blogId);
            if (blog != null)
            {
                dbContext.Blogs.Remove(blog);
                var result = dbContext.SaveChanges();
                if (result > 0)
                {
                    return Ok("Silme İşlemi Başarılı");
                }
                return BadRequest();

            }
            return BadRequest("İlgili blog mevcut değil.");

        }




    }
}
