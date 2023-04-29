using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WelcomeToTurkeyAPI.Data;
using WelcomeToTurkeyAPI.Data.Entities;
using WelcomeToTurkeyAPI.Dtos;

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
