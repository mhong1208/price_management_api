using price_management_api.Enums;

namespace price_management_api.Entities;

public class Supplier : BaseEntity {
    public required string SupplierCode { get; set; }
    public required string SupplierName { get; set; }
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? TaxCode { get; set; }
    public string? Description { get; set; }
    public ItemStatus Status { get; set; } = ItemStatus.ACTIVE;
    // Relationship: A supplier provides multiple items at specific prices.
    public ICollection<ItemPrice> ItemPrices { get; set; } = [];
}