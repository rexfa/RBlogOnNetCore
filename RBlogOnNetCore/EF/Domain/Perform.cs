using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.EF.Domain
{
    public class Perform : BaseEntity
    {
        public string name { get; set; }
        public string bigPicture { get; set; }
        public string smallPicture { get; set; }
        public string webUrl { get; set; }
        public string description { get; set; }
        public int groupId { get; set; }
        public int sort { get; set; }
        public DateTime createdOn { get; set; }
    }
}
