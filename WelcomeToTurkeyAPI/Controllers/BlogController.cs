using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WelcomeToTurkeyAPI.Data.Entities;
using WelcomeToTurkeyAPI.Dtos;
using WelcomeToTurkeyAPI.Dtos.CommentDtos;

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

        [HttpGet("list-blog")]
        [AllowAnonymous]
        public IActionResult ListAllBlogs()
        {
            var blogs = dbContext.Blogs.Join(dbContext.Categories,
                b => b.CategoryId,
                c => c.Id,
                (b, c) => new ListAllBlogsDto
                {
                    BlogId = b.Id,
                    Category=c.CategoryName,
                    Content=b.Content,
                    Title = b.Title,    
                    PublishDate=b.PublishDate,

                }).ToList();



            return Ok(blogs);
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
