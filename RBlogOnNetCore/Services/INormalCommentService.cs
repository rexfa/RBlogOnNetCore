using System.Collections.Generic;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.Services
{
    public interface INormalCommentService
    {
        NormalComment CreateNormalComment(NormalComment normalComment);
        IList<NormalComment> GetNormalComments(int blogId, int size);
    }
}
