using price_management_api.Enums;

namespace price_management_api.DTOs;

public class ItemDto
{
    public Guid Id { get; set; }
    public string? ItemCode { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Unit { get; set; }
    public string? Category { get; set; }
    public ItemStatus Status { get; set; } = ItemStatus.ACTIVE;
}

public class CreateItemDto
{
    public string? ItemCode { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Unit { get; set; }
    public string? Category { get; set; }
    public ItemStatus Status { get; set; } = ItemStatus.ACTIVE;
}
