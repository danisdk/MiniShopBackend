using Microsoft.EntityFrameworkCore;

namespace MiniShop.Services;

public interface IPaginationService<T>
{
    Task<PagedResult<T>> GetPagedAsync(
        IQueryable<T> query,
        int pageNumber,
        int pageSize);
}

public class PaginationService<T> : IPaginationService<T>
{
    public async Task<PagedResult<T>> GetPagedAsync(
        IQueryable<T> query,
        int pageNumber,
        int pageSize)
    {
        var total = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T> { Items = items, TotalCount = total, PageNumber = pageNumber, PageSize = pageSize};
    }
}

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => 
        (int)Math.Ceiling((double)TotalCount / PageSize);
}