using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.EF.Domain
{
    public class Customer:BaseEntity
    {
        //private ICollection<Picture> _pictures;
        public byte[] guid { set; get; }
        public string name { set; get; }
        public string password { set; get; }
        public string salt { set; get; }
        public DateTime createdOn { set; get; }

        #region Navigation properties
        public virtual ICollection<Blog> Blogs { set; get; }
        //public virtual ICollection<Picture> Pictures
        //{
        //    get { return _pictures ?? (_pictures = new List<Picture>()); }
        //    protected set { _pictures = value; }
        //}
        public virtual ICollection<Picture> Pictures { get; set; }

        #endregion
    }
}
