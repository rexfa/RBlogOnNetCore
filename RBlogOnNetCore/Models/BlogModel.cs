using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.Models
{
    public class BlogModel
    {
        public int Id { get; set; }
        public string Title { set; get; }
        public string Content { set; get; }
        public DateTime CreatedOn { get; set; }
        public DateTime ReleasedOn { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { set; get; }
    }
}
