using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.EF.Domain
{
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
