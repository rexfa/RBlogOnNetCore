using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RBlogOnNetCore.EF.Domain;
using Microsoft.Extensions.Caching.Memory;
using RBlogOnNetCore.Configuration;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;

namespace RBlogOnNetCore.Services
{
    public class BlogService : IBlogService
    {
        private readonly MysqlContext _mysqlContext;
        private readonly EfRepository<Blog> _blogEfRepository;
        private readonly EfRepository<BlogTagMapper> _blogTagMapperEfRepository;
        private readonly IMemoryCache _memoryCache;
        public BlogService(MysqlContext mysqlContext, IMemoryCache memoryCache)
        {
            _mysqlContext = mysqlContext;
            _blogEfRepository = new EfRepository<Blog>(this._mysqlContext);
            _blogTagMapperEfRepository = new EfRepository<BlogTagMapper>(this._mysqlContext);
            _memoryCache = memoryCache;
        }

        public IList<Blog> GetBlogsByTagId(int tagId)
        {
            var blogTagMappers = _blogTagMapperEfRepository.Table.Where(x => x.TagId == tagId).ToList();
            if (blogTagMappers != null)
            {
                var blogIds = blogTagMappers.Select(bt => { return bt.BlogId; }).ToArray();
                var blogs = _blogEfRepository.Table.Where(b => blogIds.Contains(b.Id)).OrderByDescending(b=>b.ReleasedOn).ToList();
                return blogs;
            }
            else
            {
                return null;
            }
        }
    }
}
