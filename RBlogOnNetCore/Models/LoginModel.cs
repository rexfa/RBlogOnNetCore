using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.Models
{
    public class LoginModel
    {
        public string Name { get; set; }
        public string password { get; set; }
        public bool RememberMe { get; set; }
    }
}
