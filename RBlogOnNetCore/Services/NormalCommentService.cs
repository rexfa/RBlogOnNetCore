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
    public class NormalCommentService : INormalCommentService
    {
        private readonly MysqlContext _mysqlContext;
        private readonly EfRepository<NormalComment> _normalCommentRepository;
        private readonly IMemoryCache _memoryCache;
        public NormalCommentService(MysqlContext mysqlContext, IMemoryCache memoryCache)
        {
            _mysqlContext = mysqlContext;
            _normalCommentRepository = new EfRepository<NormalComment>(this._mysqlContext);
            _memoryCache = memoryCache;
        }

        public IList<NormalComment> GetNormalComments(int blogId, int size)
        {
            var comments = _memoryCache.GetOrCreate(RBMemCacheKeys.NORMALCOMMENTKEY + blogId.ToString(),entry=> {
                var query = _normalCommentRepository.Table.Where(c=> blogId != 0 ? c.BlogId == blogId : c.BlogId != -1 & c.IsDeleted==false)
                    .OrderByDescending(c => c.CreatedOn).Take(size);
                var cs = query.ToList();
                return cs;
            });
            return comments;
        }

        public NormalComment CreateNormalComment(NormalComment normalComment)
        {
            _normalCommentRepository.Insert(normalComment);
            _mysqlContext.SaveChanges();
            _memoryCache.Remove(RBMemCacheKeys.NORMALCOMMENTKEY + normalComment.BlogId.ToString());
            _memoryCache.Remove(RBMemCacheKeys.NORMALCOMMENTKEY + "0");
            return normalComment;
        }
    }
}
