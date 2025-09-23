using System.ComponentModel.DataAnnotations;

namespace Common.Common.Models;

public sealed class PageModel
{
    [Range(1, int.MaxValue)] public int Page { get; set; } = 1;

    [Range(1, 100)] public int PageSize { get; set; } = 10;

    public static PageModel All => new()
        { Page = 1, PageSize = int.MaxValue };

    public static PageModel Max => new()
        { Page = 1, PageSize = 100 };

    public static PageModel Single => new()
        { Page = 1, PageSize = 1 };

    public static PageModel Default => new()
        { Page = 1, PageSize = 10 };

    public static PageModel Count => new()
        { Page = -1, PageSize = -1 };
}

public class PageModelResult<T> : IResultDtoBase
{
    public int Total { get; set; }
    public IEnumerable<T> Items { get; set; }
    public List<WarningModelResultEntry> Warnings { get; set; }

    public string TraceId { get; set; }
}