using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RBlogOnNetCore.EF.Domain;
using Microsoft.Extensions.Caching.Memory;
using RBlogOnNetCore.Configuration;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;
using Microsoft.Extensions.Options;

namespace RBlogOnNetCore.Services
{
    public interface IBlogBannerService
    {
        IList<BlogBanner> GetBlogBanners();
        BlogBanner AddBlogBanner(BlogBanner blogBanner);
        BlogBanner UpdateBlogBanner(BlogBanner blogBanner);
    }
}
