using price_management_api.Enums;

namespace price_management_api.DTOs;

public class ItemDetailDto
{
    public Guid Id { get; set; }
    public string ItemCode { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Unit { get; set; }
    public string? Category { get; set; }
    public ItemStatus Status { get; set; } = ItemStatus.ACTIVE;
    public List<ItemPriceDetailDto> SupplierPrices { get; set; } = [];
}

public class ItemPriceDetailDto
{
    public string Id { get; set; } = string.Empty;
    public SupplierDto? Supplier { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string EffectiveDate { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}
