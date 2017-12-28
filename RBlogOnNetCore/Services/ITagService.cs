using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RBlogOnNetCore.EF.Domain;
using RBlogOnNetCore.EF;

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
    }
}
