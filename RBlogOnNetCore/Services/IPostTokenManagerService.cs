namespace RBlogOnNetCore.Services
{
    public interface IPostTokenManagerService
    {
        void SetPostToken(string PostToken);
        bool CheckPostToken(string PostToken);
        bool CheckAndDelPostToken(string PostToken);
        void DelPostToken(string PostToken);
        string GetNewPostToken(string seed);
    }
}
