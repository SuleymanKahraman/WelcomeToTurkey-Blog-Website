
namespace WelcomeToTurkeyAPI.Dtos
{
    public class ListAllBlogsDto
    {
        public int BlogId { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
