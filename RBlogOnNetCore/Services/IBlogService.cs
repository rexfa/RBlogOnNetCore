using System;
using System.Collections.Generic;
using RBlogOnNetCore.EF.Domain;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.Models;

namespace RBlogOnNetCore.Services
{
    public interface IBlogService
    {
        IList<Blog> GetBlogsByTagId(int tagId);
        Blog InsertBlog(BlogModel blogModel);
        void GetPagedBlogsByTagId(int tagId,int PageIndex,int SegmentIndex);

    }
}
