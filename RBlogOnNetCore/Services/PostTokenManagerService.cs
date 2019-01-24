using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using RBlogOnNetCore.Configuration;

namespace RBlogOnNetCore.Services
{
    public class PostTokenManagerService : IPostTokenManagerService
    {
        private readonly IMemoryCache _memoryCache;
        private const string TOKENKEY = "PostToken.";
        private const string ALLTOKENKEYS = "PostToken.AllKeys.List";
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
            DelKeyInAllKeys(getKey(PostToken));
        }

        public void SetPostToken(string PostToken)
        {
            _memoryCache.Set<string>(getKey(PostToken), PostToken,TimeSpan.FromHours(1));
            PushKeyInAllKeys(getKey(PostToken));
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
            return TOKENKEY + PostToken;
        }
        private List<string> getAllKeys()
        {
            return _memoryCache.GetOrCreate<List<string>>(ALLTOKENKEYS, entry => { return new List<string>(); });
        }
        private void PushKeyInAllKeys(string key)
        {
            List<string> allkeys = getAllKeys();
            //对于过多的key就直接野蛮清空所有key和token
            if (allkeys.Count > 2000)
            {
                ClearTokenPool();
                allkeys = getAllKeys();
            }
            if (!allkeys.Contains(key))
                allkeys.Add(key);
            _memoryCache.Set(ALLTOKENKEYS, allkeys,TimeSpan.FromHours(1));
        }
        private void DelKeyInAllKeys(string key)
        {
            List<string> allkeys = getAllKeys();
            if (allkeys.Contains(key))
                allkeys.Remove(key);
            _memoryCache.Set(ALLTOKENKEYS, allkeys, TimeSpan.FromHours(1));
        }
        private void ClearTokenPool()
        {
            List<string> allkeys = getAllKeys();
            foreach (var key in allkeys)
            {
                DelPostToken(key);
            }
            _memoryCache.Remove(ALLTOKENKEYS);
        }
    }
}
