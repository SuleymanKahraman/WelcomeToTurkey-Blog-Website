namespace WelcomeToTurkeyAPI.Dtos.BlogDtos
{
    public class GetBlogByIdDto
    {
        public string Category { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsPublished { get; set; }
        public string Photo { get; set; }

    }
}


