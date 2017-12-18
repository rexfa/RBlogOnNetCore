using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace RBlogOnNetCore.Services
{
    public class TagService : ITagService
    {
        private readonly MysqlContext _mysqlContext;
        private readonly EfRepository<Tag> _tagEfRepository;
        private readonly EfRepository<BlogTagMapper> _blogTagMapperEfRepository;

        public TagService(MysqlContext mysqlContext)
        {
            _mysqlContext = mysqlContext;
            _tagEfRepository = new EfRepository<Tag>(this._mysqlContext);
            _blogTagMapperEfRepository = new EfRepository<BlogTagMapper>(this._mysqlContext);
        }
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
            var hotTags = _tagEfRepository.Table.OrderByDescending(t => t.ReferenceNum).Take(5).ToList();
            return hotTags;
        }

        public Tag GetTagByName(string tagName)
        {
            var tag = _tagEfRepository.Table.Where(t => t.TagName == tagName).First();
            return tag;
        }

        public void SetTagsToBlog(int blogId, string tagNameString)
        {
            var tagNames = tagNameString.Split(',');
            var existenceTags = _tagEfRepository.Table.Where(t => tagNames.Contains(t.TagName)).ToList();
            var exitebceTagNames = existenceTags.Select(t => { return t.TagName; }).ToArray();
            var non_existentTagNames = tagNames.Where(name => !exitebceTagNames.Contains(name)).ToArray();

            List<BlogTagMapper> bts = new List<BlogTagMapper>();
            //foreach()

        }
        public Tag CreatTag(string tagName)
        {
            var tag = GetTagByName(tagName);
            if (tag != null)
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
    }
}
