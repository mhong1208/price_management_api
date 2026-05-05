namespace price_management_api.Entities;

public class Supplier : BaseEntity {
    public required string SupplierCode { get; set; }
    public required string SupplierName { get; set; }
    public string? Email { get; set; }
    // ... các trường khác theo đề bài

    public ICollection<ItemPrice> ItemPrices { get; set; } = [];
}