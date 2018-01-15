
using System.Collections;
using System.Collections.Generic;

namespace RBlogOnNetCore.Infrastructures
{
    public interface IRPageable: IEnumerable
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalPages { get; }
        int SegmentIndex { get; }
        int SegmentSize { get; }
        int SegmentBeginIndex { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
    public interface IRPagedList<T> : IList<T>, IRPageable
    {

    }
}
