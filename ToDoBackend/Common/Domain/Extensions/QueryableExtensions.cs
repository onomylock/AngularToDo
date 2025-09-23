using Common.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Domain.Extensions;

public static class QueryableExtensions
{
    public static async Task<(int total, IQueryable<T> query)> GetPage<T>(this IQueryable<T> query, PageModel model,
        CancellationToken cancellationToken = default) where T : class
    {
        if (model == PageModel.Count)
            return (await query.CountAsync(cancellationToken), null);

        return (
            await query.CountAsync(cancellationToken),
            query
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
        );
    }

    public static IQueryable<T> GetPage<T>(this IOrderedQueryable<T> query, PageModel model) where T : class
    {
        return query
            .Skip((model.Page - 1) * model.PageSize)
            .Take(model.PageSize);
    }

    public static IQueryable<T> Transform<T>(
        this IQueryable<T> query,
        Func<IQueryable<T>, IQueryable<T>> queryTransform
    )
    {
        return queryTransform(query);
    }
}