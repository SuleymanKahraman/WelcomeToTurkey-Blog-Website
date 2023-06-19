using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WelcomeToTurkeyAPI.Data.Entities;
using WelcomeToTurkeyAPI.Dtos;
using WelcomeToTurkeyAPI.Dtos.CommentDtos;
using WelcomeToTurkeyAPI.Dtos.BlogDtos;

namespace WelcomeToTurkeyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BlogController : ControllerBase
    {
        private readonly WTTDbContext dbContext;

        public BlogController(WTTDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /* ADD PROCESS */

        //TODO: add-comment
        [HttpPost("add-comment")]
        [Authorize]
        public IActionResult AddNewComment([FromBody] AddNewCommentDto dto)
        {
            var entity = new Comment()
            {
                Message = dto.Message,
                UserId = dto.UserId,
                BlogId = dto.BlogId,
                CommentDate = DateTime.Now
            };
            dbContext.Comments.Add(entity);
            var result = dbContext.SaveChanges();
            if (result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }

        /* LIST PROCESS */

        //TODO: list-comment

        [HttpGet("list-comment/{blogId}")]
        public IActionResult ListAllComment([FromRoute] int blogId)
        {
            var comments = dbContext.Comments.Where(x => x.BlogId == blogId).Select(x => new ListAllCommentDto
            {
                CommentDate = x.CommentDate.ToString("dd MMM yyyy HH:mm"),
                CommentId = x.Id,
                FullName = $"{x.User.FirstName} {x.User.LastName}",
                Message = x.Message,
                UserId = x.UserId
            }).ToList();
            return Ok(comments);
        }

        //TODO: list-blog

        [HttpGet("list-blog")]
        public IActionResult ListAllBlogs()
        {
            var blogs = dbContext.Blogs.Where(b => b.IsPublished == true).Select(b => new ListAllBlogsDto
            {
                BlogId = b.Id,
                Title = b.Title,
                PublishDate = b.PublishDate.ToShortDateString(),
                Category = b.Category.CategoryName,
                Photo = b.Photo != null ? Convert.ToBase64String(b.Photo) : null
            }).ToList();
            if (blogs is not null)
            {
                return Ok(blogs);
            }
            return BadRequest();

        }
        [HttpGet("get-blogs-by-categoryid/{categoryId}")]
        public IActionResult ListAllBlogsByCategory([FromRoute] int categoryId)
        {
            var blogs = dbContext.Blogs.Where(b => b.IsPublished == true && b.CategoryId == categoryId).Select(b => new ListAllDetailedBlogsDto
            {
                BlogId = b.Id,
                Title = b.Title,
                PublishDate = b.PublishDate.ToShortDateString(),
                Category = b.Category.CategoryName,
                Photo = b.Photo != null ? Convert.ToBase64String(b.Photo) : null,
                Summary = b.Content.Substring(0, 200) + "..."
            }).ToList();
            if (blogs is not null)
            {
                return Ok(blogs);
            }
            return BadRequest();

        }

        /* UPDATE PROCESS */

        //TODO: update-comment-by-Id

        [HttpPut("update-comment")]
        [Authorize]
        public IActionResult UpdateComment([FromBody] UpdateCommentDto comment)
        {
            var currentComment = dbContext.Comments.SingleOrDefault(x => x.Id == comment.CommentId);
            if (currentComment != null)
            {
                currentComment.Message = comment.Message;
                currentComment.CommentDate = DateTime.Now;
                var result = dbContext.SaveChanges();
                if (result > 0)
                {
                    return Ok("Güncelleme Başarılı...");
                }
                return BadRequest();
            }
            return Ok("Yorum Bulunamadı!!!");
        }

        /* DELETE PROCESS */

        //TODO: delete-comment-by-Id
        [HttpDelete("delete-comment/{commentId}")]
        [Authorize]
        public IActionResult DeleteCommentById([FromRoute] int commentId)
        {
            var entity = dbContext.Comments.FirstOrDefault(x => x.Id == commentId);
            dbContext.Comments.Remove(entity);
            var result = dbContext.SaveChanges();
            if (result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
