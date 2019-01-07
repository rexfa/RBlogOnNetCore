using System.Collections.Generic;
using RBlogOnNetCore.EF.Domain;
using RBlogOnNetCore.Models;

namespace RBlogOnNetCore.Services
{
    public interface IBlogService
    {
        IList<Blog> GetBlogsByTagId(int tagId);
        Blog InsertBlog(BlogModel blogModel);
        BlogPagingModel GetPagedBlogsByTagId(int tagId, int pageIndex, int pageSize, int customerId);

    }
}
