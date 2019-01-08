namespace RBlogOnNetCore.Models
{
    public class NormalCommentModel
    {
        public int Id { get; set; }
        public string Nikename { get; set; }
        public string CommentText { get; set; }
        public string Email { get; set; }
        public string HomepageUrl { get; set; }
        public string PreIds { get; set; }
        public int BlogId { get; set; }
        public string CreatedOnStr { get; set; }
        public bool IsDeleted { get; set; }
    }
}
