namespace RBlogOnNetCore.Services
{
    public interface IMemCacheService
    {
        void ClearMemCache(string key = null);
    }
}
