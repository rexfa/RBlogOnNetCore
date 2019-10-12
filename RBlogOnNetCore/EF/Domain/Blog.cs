using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBlogOnNetCore.EF.Domain
{
    [Table("Blog")]
    public class Blog:BaseEntity
    {
        public string Title { set; get; }
        public string Content { set; get; }
        public int CustomerId { set; get; }
        public bool IsDeleted { get; set; }
        public bool IsReleased { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ReleasedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string ImageIds { get; set; }
        public Customer Customer { set; get; }
        public virtual ICollection<BlogTagMapper> BlogTagMappers { get; set; }
    }
}
