namespace DaisyBlazor;

/// <summary>
/// Non-generic view of a <see cref="Table{T}"/> so the non-generic
/// <c>TablePager</c> can drive paging without knowing the row type.
/// </summary>
public interface ITablePagerHost
{
    int TotalItems { get; }

    int CurrentPage { get; }

    int PageCount { get; }

    int PageSize { get; }

    void SetPage(int page);

    void SetPageSize(int size);
}
