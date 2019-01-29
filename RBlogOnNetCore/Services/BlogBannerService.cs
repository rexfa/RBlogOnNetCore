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
    public class BlogBannerService : IBlogBannerService
    {
        private readonly MysqlContext _mysqlContext;
        private readonly EfRepository<BlogBanner> _blogBannerRepository;
        private readonly EfRepository<BlogTagMapper> _blogTagMapperRepository;
        private readonly IMemoryCache _memoryCache;

        public BlogBannerService(MysqlContext mysqlContext, IMemoryCache memoryCache)
        {
            _mysqlContext = mysqlContext;
            _blogBannerRepository = new EfRepository<BlogBanner>(this._mysqlContext);
        }
        public BlogBanner AddBlogBanner(BlogBanner blogBanner)
        {
            _blogBannerRepository.Insert(blogBanner);
            return blogBanner;
        }

        public IList<BlogBanner> GetBlogBanners()
        {
            return _blogBannerRepository.Table.Where(bb => bb.RetiredOn < DateTime.Now || bb.Type == 1).OrderBy(bb => bb.SortNum).ToList();
        }
    }
}
