using System;
using System.Collections.Generic;

namespace GizmoGateway.Domain.Common;

public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalCount { get; }

    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling(TotalCount / (double)PageSize) : 0;

    public PagedResponse(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}
