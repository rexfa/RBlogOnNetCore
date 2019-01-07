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

        public IList<NormalComment> GetNormalComments(int BlogId, int size)
        {
            throw new NotImplementedException();
        }

        public NormalComment Insert(NormalComment normalComment)
        {
            throw new NotImplementedException();
        }
    }
}
