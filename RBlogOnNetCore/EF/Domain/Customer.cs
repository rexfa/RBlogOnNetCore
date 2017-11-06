using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.EF.Domain
{
    public class Customer:BaseEntity
    {
        public byte[] guid { set; get; }
        public string name { set; get; }
        public string password { set; get; }
        public string salt { set; get; }
        public DateTime createdOn { set; get; }

        public List<Blog> Blogs { set; get; }
    }
}
