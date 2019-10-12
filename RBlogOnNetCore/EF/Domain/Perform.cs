using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBlogOnNetCore.EF.Domain
{
    [Table("Perform")]
    public class Perform : BaseEntity
    {
        public string Name { get; set; }
        public string BigPicture { get; set; }
        public string SmallPicture { get; set; }
        public string WebUrl { get; set; }
        public string Description { get; set; }
        public int GroupId { get; set; }
        public int Sort { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
