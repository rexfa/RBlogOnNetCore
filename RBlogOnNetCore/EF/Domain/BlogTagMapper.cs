using System.ComponentModel.DataAnnotations.Schema;

namespace RBlogOnNetCore.EF.Domain
{
    [Table("BlogTagMapper")]
    public class BlogTagMapper : BaseEntity
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int Sort { get; set; }
    }
}
