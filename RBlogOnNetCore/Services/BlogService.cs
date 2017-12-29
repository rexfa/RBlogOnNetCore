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
        private readonly EfRepository<Blog> _blogRepository;
        private readonly EfRepository<BlogTagMapper> _blogTagMapperRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ITagService _tagService;
        public BlogService(MysqlContext mysqlContext, IMemoryCache memoryCache, ITagService tagService)
        {
            _mysqlContext = mysqlContext;
            _blogRepository = new EfRepository<Blog>(this._mysqlContext);
            _blogTagMapperRepository = new EfRepository<BlogTagMapper>(this._mysqlContext);
            _memoryCache = memoryCache;
            _tagService = tagService;
        }

        public IList<Blog> GetBlogsByTagId(int tagId)
        {
            var blogTagMappers = _blogTagMapperRepository.Table.Where(x => x.TagId == tagId).ToList();
            if (blogTagMappers != null)
            {
                var blogIds = blogTagMappers.Select(bt => { return bt.BlogId; }).ToArray();
                var blogs = _blogRepository.Table.Where(b => blogIds.Contains(b.Id)).OrderByDescending(b=>b.ReleasedOn).ToList();
                return blogs;
            }
            else
            {
                return null;
            }
        }

        public Blog InsertBlog(BlogModel blogModel)
        {
            DateTime now = DateTime.Now;
            var blog = new Blog()
            {
                Title = blogModel.Title,
                CustomerId = blogModel.CustomerId,
                Content = blogModel.Content,
                CreatedOn = now,
                UpdatedOn = now,
                ReleasedOn = now,
                IsDeleted = false,
                IsReleased = true
            };
            _blogRepository.Insert(blog);
            _mysqlContext.SaveChanges();
            _tagService.SetTagsToBlog(blog.Id, blogModel.Tags);
            return blog;
        }
    }
}
