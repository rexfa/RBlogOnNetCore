using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.Services
{
    public interface IMemCacheService
    {
        void ClearMemCache(string key = null);
    }
}
