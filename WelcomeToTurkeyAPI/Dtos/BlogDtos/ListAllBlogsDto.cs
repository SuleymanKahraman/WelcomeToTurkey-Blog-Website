namespace WelcomeToTurkeyAPI.Dtos.BlogDtos
{
    public class ListAllBlogsDto
    {
        public int BlogId { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string PublishDate { get; set; }
        public string Photo { get; set; }
    }
    public class ListAllDetailedBlogsDto : ListAllBlogsDto
    {
        public string Summary { get; set; }
    }
}


