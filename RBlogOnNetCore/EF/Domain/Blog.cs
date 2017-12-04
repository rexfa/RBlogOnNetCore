using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.EF.Domain
{
    public class Blog:BaseEntity
    {
        public string title { set; get; }
        public string content { set; get; }
        public int customerId { set; get; }
        public bool isDeleted { get; set; }
        public bool isReleased { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime releasedOn { get; set; }
        public DateTime updatedOn { get; set; }
        public Customer Customer { set; get; }
    }
}
