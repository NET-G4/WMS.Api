using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WMS.Services.Common;

namespace WMS.Services.Extensions;

internal static class QueryableExtensions
{
    public static PaginatedList<T> ToPaginatedList<T, K>(
        this IQueryable<K> query,
        IConfigurationProvider configurationProvider,
        int pageNumber = 1,
        int pageSize = 15)
    {
        var count = query.Count();
        var data = query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<T>(configurationProvider)
            .ToList();

        return new PaginatedList<T>(data, pageNumber, pageSize, count);
    }
}
