using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBlogOnNetCore.EF.Domain
{
    [Table("Picture")]
    public class Picture : BaseEntity
    {
        public string LocalName { get; set; }
        public string CustomName { get; set; }
        public string OriginalName { get; set; }
        public int CustomerId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string PicType { get; set; }
        public Customer Customer { set; get; }
    }
}
