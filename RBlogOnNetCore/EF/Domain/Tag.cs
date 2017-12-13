using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.EF.Domain
{
    public class Tag : BaseEntity
    {
        public string tag { get; set; }
        public int referenceNum { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime lastReferencedOn { get; set; }
    }
}
