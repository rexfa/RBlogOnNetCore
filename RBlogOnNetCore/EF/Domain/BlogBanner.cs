using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace RBlogOnNetCore.EF.Domain
{
    [Table("BlogBanner")]
    public class BlogBanner:BaseEntity
    {
        public string Title { set; get; }
        public string PicUrl { set; get; }
        public string Url { set; get; }
        public DateTime CreatedOn { set; get; }
        public DateTime RetiredOn { set; get; }
        public int SortNum { set; get; }
        public int Type { set; get; }
    }
}
