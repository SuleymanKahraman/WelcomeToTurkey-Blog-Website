namespace WelcomeToTurkeyAPI.Dtos.BlogDtos
{
    public class ListBlogsByFilter
    {
        public int BlogId { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string PublishDate { get; set; }
        public bool IsPublished { get; set; }
    }
}


