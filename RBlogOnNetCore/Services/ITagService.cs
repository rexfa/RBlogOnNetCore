using System.Collections.Generic;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.Services
{
    public interface ITagService
    {
        IList<Tag> GetHotTags();
        IList<Tag> GetBlogTags(int blogId);
        void SetTagsToBlog(int blogId, string tags);
        Tag GetTagByName(string tagName);
        Tag CreatTag(string tagName);
        Tag GetTagById(int Id);
        void ClearTagsCache(string key = null);
    }
}
