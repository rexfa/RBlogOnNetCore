using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.EF.Domain
{
    public class Picture : BaseEntity
    {
        public string localName { get; set; }
        public string customName { get; set; }
        public string originalName { get; set; }
        public int customerId { get; set; }
        public DateTime updatedOn { get; set; }
        public bool isDeleted { get; set; }
        public string picType { get; set; }
        public Customer Customer { set; get; }
    }
}
