using System;
using Microsoft.Extensions.Caching.Memory;
using RBlogOnNetCore.Configuration;

namespace RBlogOnNetCore.Services
{
    public class PostTokenManagerService : IPostTokenManagerService
    {
        private readonly IMemoryCache _memoryCache;
        private const string Key = "PostToken.";
        public PostTokenManagerService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public bool CheckAndDelPostToken(string PostToken)
        {
            bool r = CheckPostToken(PostToken);
            if (r)
                DelPostToken(PostToken);
            return r;
        }

        public bool CheckPostToken(string PostToken)
        {
            object t = _memoryCache.Get(getKey(PostToken));
            if (t == null)
                return false;
            else if (PostToken == t.ToString())
                return true;
            else
                return false;
        }

        public void DelPostToken(string PostToken)
        {
            _memoryCache.Remove(getKey(PostToken));
        }

        public void SetPostToken(string PostToken)
        {
            _memoryCache.Set<string>(getKey(PostToken), PostToken,TimeSpan.FromHours(2));
        }
        public string GetNewPostToken(string seed)
        {
            Guid g1 = Guid.NewGuid();
            string token = g1.ToString() + seed;
            SetPostToken(token);
            return token;
        }
        private string getKey(string PostToken)
        {
            return Key + PostToken;
        }

    }
}
