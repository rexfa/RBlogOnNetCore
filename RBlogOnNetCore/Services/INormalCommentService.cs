using System.Collections.Generic;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.Services
{
    public interface INormalCommentService
    {
        NormalComment CreateNormalComment(NormalComment normalComment);
        IList<NormalComment> GetNormalComments(int blogId, int size);
        /// <summary>
        /// 过滤替换字符串
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        string ReplacementFilter(string original);
    }
}
