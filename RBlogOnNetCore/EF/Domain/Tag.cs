using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.EF.Domain
{
    public class Tag : BaseEntity
    {
        public string TagName { get; set; }
        public int ReferenceNum { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastReferencedOn { get; set; }

        public virtual ICollection<BlogTagMapper> BlogTagMappers { get; set; }
    }
}
