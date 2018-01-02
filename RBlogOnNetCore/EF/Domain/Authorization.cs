using System;
using System.Collections.Generic;

namespace RBlogOnNetCore.EF.Domain
{
    public class Authorization : BaseEntity
    {
        public string URL { get; set; }
        public string URLController { get; set; }
        public string URLAction { get; set; }
        public bool IsNeedAuthorization { get; set; }
        public string UnauthorizedJump { get; set; }
    }
}
