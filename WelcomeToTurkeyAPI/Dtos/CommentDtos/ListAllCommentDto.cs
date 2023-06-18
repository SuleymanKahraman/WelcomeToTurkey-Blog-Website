namespace WelcomeToTurkeyAPI.Dtos.CommentDtos
{
    public class ListAllCommentDto
    {
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public string Message { get; set; }
        public string FullName { get; set; }
        public string CommentDate { get; set; }
    }
}
