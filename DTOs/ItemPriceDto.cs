namespace price_management_api.DTOs;

public class ItemPriceDto
{
    public string Id { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;
    public ItemDto? Item { get; set; }
    public string SupplierId { get; set; } = string.Empty;
    public SupplierDto? Supplier { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string EffectiveDate { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}

public class CreateItemPriceDto
{
    public string ItemId { get; set; } = string.Empty;
    public string SupplierId { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string EffectiveDate { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
