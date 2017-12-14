
using System.Collections;
using System.Collections.Generic;

namespace RBlogOnNetCore.Infrastructures
{
    public interface IPageable : IEnumerable
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
    public interface IPagedList<T> : IList<T>, IPageable
    {

    }
}
