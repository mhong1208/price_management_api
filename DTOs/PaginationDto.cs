using price_management_api.Enums;

namespace price_management_api.DTOs;

public class PaginationRequestDto
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
    public string? SearchText { get; set; }
    public string? Category { get; set; }
    public ItemStatus? Status { get; set; }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
}
