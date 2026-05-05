namespace price_management_api.DTOs;

public class ItemPriceHistoryDto
{
    public Guid Id { get; set; }
    public Guid ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public Guid SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string EffectiveDate { get; set; } = string.Empty;
    public string? Action { get; set; }
    public string? Notes { get; set; }
    public string CreatedAt { get; set; } = string.Empty;
}
