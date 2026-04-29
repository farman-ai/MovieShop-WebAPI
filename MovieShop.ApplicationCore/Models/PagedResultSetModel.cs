using System;
using System.Collections.Generic;

namespace MovieShop.ApplicationCore.Models;

public class PagedResultSetModel<T> where T : class
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
    public IEnumerable<T> Data { get; }

    public PagedResultSetModel(IEnumerable<T> data, int pageIndex, int pageSize, int totalCount)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        Data = data;
    }
}