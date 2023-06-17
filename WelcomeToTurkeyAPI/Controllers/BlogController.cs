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
    [Authorize]
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
        public IActionResult AddNewComment([FromBody] AddNewCommentDto dto)
        {
            var entity = new Comment() { Message = dto.Message, UserId = dto.UserId, BlogId = dto.BlogId, CommentDate = DateTime.Now };
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
                CommentDate = x.CommentDate,
                CommentId = x.Id,
                FullName = $"{x.User.FirstName} {x.User.LastName}",
                Message = x.Message,
                UserId = x.UserId
            }).ToList();
            return Ok(comments);
        }

        //TODO: list-blog

        [HttpGet("list-blog")]
        [AllowAnonymous]
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
            if(blogs is not null)
            {
                return Ok(blogs);
            }
            return BadRequest();

        }
        [HttpDelete("delete-comment/{commentId}")]
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
