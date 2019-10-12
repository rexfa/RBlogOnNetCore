using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBlogOnNetCore.EF.Domain
{
    [Table("NormalComment")]
    public class NormalComment : BaseEntity
    {
        public string Nikename { get; set; }
        public string CommentText { get; set; }
        public string Email { get; set; }
        public string HomepageUrl { get; set; }
        public string PreIds { get; set; }
        public int BlogId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
