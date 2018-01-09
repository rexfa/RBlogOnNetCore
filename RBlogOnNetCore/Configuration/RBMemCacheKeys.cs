using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.Configuration
{
    public struct RBMemCacheKeys
    {
        public static string HOTTAGSKEY = "memcachekey.tags.hottags";
        public static string CUSTOMERAUTHORIZATIONKEY = "memcachekey.customers.authorizations.";
        public static string CUSTOMERROLEKEY = "memcachekey.customers.roles.";
    }
}
