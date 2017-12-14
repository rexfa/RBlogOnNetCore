using System.Collections;
using System.Collections.Generic;

namespace RBlogOnNetCore.Infrastructures
{
    public interface IPageableModel : IEnumerable
    {
        int PageIndex { get; }
        int PageNumber { get; }
        int PageSize { get; }
        int TotalItems { get; }
        int TotalPages { get; }
        int FirstItem { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
    public interface IPagination<T> : IPageableModel, IEnumerable<T>
    {

    }
}
