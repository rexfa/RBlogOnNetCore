using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBlogOnNetCore.EF.Domain
{
    [Table("Customer")]
    public class Customer:BaseEntity
    {
        //private ICollection<Picture> _pictures;
        public byte[] Guid { set; get; }
        public string Name { set; get; }
        public string Password { set; get; }
        public string Salt { set; get; }
        public DateTime CreatedOn { set; get; }
        public string CustomerName { set; get; }

        #region Navigation properties
        public virtual ICollection<Blog> Blogs { set; get; }
        //public virtual ICollection<Picture> Pictures
        //{
        //    get { return _pictures ?? (_pictures = new List<Picture>()); }
        //    protected set { _pictures = value; }
        //}
        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<CustomerRoleMapper> CustomerRoleMapper { get; set; }

        #endregion
    }
}
