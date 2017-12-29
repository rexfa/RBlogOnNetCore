using Microsoft.Extensions.Caching.Memory;
using RBlogOnNetCore.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.Services
{
    public class MemCacheService : IMemCacheService
    {
        private readonly IMemoryCache _memoryCache;
        public MemCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public void ClearMemCache(string key = null)
        {

            if (string.IsNullOrEmpty(key))
            {
                _memoryCache.Remove(RBMemCacheKeys.HOTTAGSKEY);
            }
            else
            {
                _memoryCache.Remove(key);
            }
            
        }
    }
}
