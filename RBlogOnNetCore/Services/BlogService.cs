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
    public class BlogService : IBlogService
    {
        private readonly MysqlContext _mysqlContext;
        private readonly EfRepository<Blog> _blogRepository;
        private readonly EfRepository<BlogTagMapper> _blogTagMapperRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ITagService _tagService;
        private readonly PageSettings _pageSettings;
        public BlogService(MysqlContext mysqlContext, IMemoryCache memoryCache, ITagService tagService, IOptions<PageSettings> option)
        {
            _mysqlContext = mysqlContext;
            _blogRepository = new EfRepository<Blog>(this._mysqlContext);
            _blogTagMapperRepository = new EfRepository<BlogTagMapper>(this._mysqlContext);
            _memoryCache = memoryCache;
            _tagService = tagService;
            _pageSettings = option.Value;
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

        /// <summary>
        /// 带翻页的获取
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="PageIndex"></param>
        /// <param name="SegmentIndex"></param>
        public BlogPagingModel GetPagedBlogsByTagId(int tagId, int pageIndex, int pageSize,int customerId)
        {
            BlogPagingModel model = new BlogPagingModel();

            var query = _blogRepository.Table;
            if (tagId != 0)
            {
                var blogTagMappers = _blogTagMapperRepository.Table.Where(x => x.TagId == tagId).ToList();
                if (blogTagMappers != null)
                {
                    var blogIds = blogTagMappers.Select(bt => { return bt.BlogId; }).ToArray();
                    query = query.Where(b => blogIds.Contains(b.Id));
                }
            }
            var blogs = query.ToList();
            model.TotalItems = blogs.Count;

            blogs = blogs.OrderByDescending(b => b.CreatedOn).Skip(pageIndex * pageSize).Take(pageSize).ToList();

            //var blogs = query.ToList();
            
            model.PageSize = pageSize;
            model.PageNumber = pageIndex+1;
            model.TotalPages = model.TotalItems / model.PageSize;

            //model.TotalPages
            model.Blogs = new List<BlogModel>();
            foreach (var b in blogs)
            {
                string cacheKey = RBMemCacheKeys.CUSTOMERINFOKEY + b.CustomerId.ToString();
                Customer customer = (Customer)_memoryCache.Get(cacheKey);
                BlogModel m = new BlogModel()
                {
                    Content = b.Content,
                    CreatedOn = b.CreatedOn,
                    CustomerId = b.CustomerId,
                    Id = b.Id,
                    ReleasedOn = b.ReleasedOn,
                    Title = b.Title,
                    CustomerName = customer != null ? customer.Name : "",
                    Editable = b.CustomerId==customerId?true : false,
                    Tags = string.Join(",", _tagService.GetBlogTags(b.Id).Select(t => { return t.TagName; }).ToList())
                };
                //if(customer!=null)

                model.Blogs.Add(m);
            }
            return model;
        }


    }
}
