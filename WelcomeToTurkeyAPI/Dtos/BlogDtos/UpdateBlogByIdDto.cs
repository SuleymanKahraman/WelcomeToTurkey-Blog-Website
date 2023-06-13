namespace WelcomeToTurkeyAPI.Dtos.BlogDtos
{
    public class UpdateBlogByIdDto
    {
        public int BlogId { get; set; }
        public int CategoryId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public bool IsPublished { get; set; }
        public IFormFile Photo { get; set; }
    }
}


