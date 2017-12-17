using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.EF.Domain
{
    public class BlogTagMapper : BaseEntity
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int Sort { get; set; }
    }
}
