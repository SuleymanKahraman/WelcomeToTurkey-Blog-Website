namespace WelcomeToTurkeyAPI.Dtos.CommentDtos
{
    public class ListAllCommentDto
    {
        public int CommentId { get; set; }
        public string Message { get; set; }
        public string FullName { get; set; }
        public DateTime CommentDate { get; set; }
        public int UserId { get; internal set; }
    }
}
