using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using RBlogOnNetCore.Configuration;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.Services
{
    public class TagService : ITagService
    {
        private readonly MysqlContext _mysqlContext;
        private readonly EfRepository<Tag> _tagEfRepository;
        private readonly EfRepository<BlogTagMapper> _blogTagMapperEfRepository;
        private readonly IMemoryCache _memoryCache;

        public TagService(MysqlContext mysqlContext, IMemoryCache memoryCache)
        {
            _mysqlContext = mysqlContext;
            _tagEfRepository = new EfRepository<Tag>(this._mysqlContext);
            _blogTagMapperEfRepository = new EfRepository<BlogTagMapper>(this._mysqlContext);
            _memoryCache = memoryCache;
        }
        //public void DBInitialization(MysqlContext mysqlContext)
        //{
        //    _mysqlContext = mysqlContext;
        //    _tagEfRepository = new EfRepository<Tag>(this._mysqlContext);
        //    _blogTagMapperEfRepository = new EfRepository<BlogTagMapper>(this._mysqlContext);
        //}
        public IList<Tag> GetBlogTags(int blogId)
        {
            var blogTagMappers = _blogTagMapperEfRepository.Table.Where(x => x.BlogId == blogId).ToList();
            if (blogTagMappers != null)
            {
                var tagIds = blogTagMappers.Select(bt => { return bt.TagId; }).ToArray();
                var tags = _tagEfRepository.Table.Where(t => tagIds.Contains(t.Id)).ToList();
                return tags;
            }
            else
            {
                return null;
            }
        }

        public IList<Tag> GetHotTags()
        {
            var hotTags = _memoryCache.GetOrCreate(RBMemCacheKeys.HOTTAGSKEY, entry => {
                var query = _tagEfRepository.Table.OrderByDescending(t => t.ReferenceNum).Take(20);
                var tags = query.ToList();
                SetTagReferenceNum(tags);
                return tags;

            });
           
            return hotTags;
        }

        public Tag GetTagByName(string tagName)
        {
            var tag = _tagEfRepository.Table.Where(t => t.TagName == tagName).FirstOrDefault();
            return tag;
        }

        public void SetTagsToBlog(int blogId, string tagNameString)
        {
            if (string.IsNullOrEmpty(tagNameString))
                return;
            if (string.IsNullOrEmpty(tagNameString.Trim()))
                return;
            var tagNames = tagNameString.Split(',');
            if (tagNames.Count() <= 0)
                return;

            var existenceTags = _tagEfRepository.Table.Where(t => tagNames.Contains(t.TagName)).ToList();
            var exitebceTagNames = existenceTags.Select(t => { return t.TagName; }).ToArray();
            var non_existentTagNames = tagNames.Where(name => !exitebceTagNames.Contains(name)).ToArray();

            List<BlogTagMapper> newBts = new List<BlogTagMapper>();
            int sort = 100;
            foreach (var newTag in non_existentTagNames)
            {
                var tag = CreatTag(newTag);
                if (tag.TagName == tagNames[0])
                    sort = 10;
                else
                    sort = 100;
                var bt = new BlogTagMapper()
                {
                    BlogId = blogId,
                    Sort = sort,
                    TagId = tag.Id
                };
                newBts.Add(bt);
            }
            foreach (var tag in existenceTags)
            {
                if (tag.TagName == tagNames[0])
                    sort = 10;
                else
                    sort = 100;
                var bt = new BlogTagMapper()
                {
                    BlogId = blogId,
                    Sort = sort,
                    TagId = tag.Id
                };
                newBts.Add(bt);
            }
            DeleteBlogTagByBlogId(blogId);
            _blogTagMapperEfRepository.InsertList(newBts);
            _mysqlContext.SaveChanges();
            ClearTagsCache(RBMemCacheKeys.HOTTAGSKEY);
        }
        public Tag CreatTag(string tagName)
        {
            var tag = GetTagByName(tagName);
            if (tag == null)
            {
                var now = DateTime.Now;
                tag = new Tag()
                {
                    TagName = tagName,
                    ReferenceNum=0,
                    CreatedOn = now,
                    LastReferencedOn = now
                };
                _tagEfRepository.Insert(tag);
                _mysqlContext.SaveChanges();
            }
            return tag;
        }
        public void DeleteBlogTagByBlogId(int blogId)
        {
            try
            {
                var bts = _blogTagMapperEfRepository.Table.Where(bt => bt.BlogId == blogId).ToList();
                _blogTagMapperEfRepository.DeleteList(bts);
                ClearTagsCache(RBMemCacheKeys.HOTTAGSKEY);
            }
            catch (Exception ex)
            {
                return;
            }
            
        }
        public Tag GetTagById(int Id)
        {
            return _tagEfRepository.GetById(Id);
        }
        #region Internal function
        private void SetTagReferenceNum(List<Tag> tags)
        {
            if(tags!=null)
            {
                foreach (var tag in tags)
                {
                    int num = _blogTagMapperEfRepository.Table.Where(tb => tb.TagId == tag.Id).Count();
                    tag.ReferenceNum = num;
                    _tagEfRepository.Update(tag);
                }
                _mysqlContext.SaveChanges();
            }
        }

        public void ClearTagsCache(string key = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                _memoryCache.Remove(RBMemCacheKeys.HOTTAGSKEY);
            }
            else
            {
                _memoryCache.Remove(key);
            }
        }
        #endregion
    }
}
