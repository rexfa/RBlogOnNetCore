using System.Collections.Generic;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.Services
{
    interface INormalCommentService
    {
        NormalComment Insert(NormalComment normalComment);
        IList<NormalComment> GetNormalComments(int BlogId, int size);
    }
}
