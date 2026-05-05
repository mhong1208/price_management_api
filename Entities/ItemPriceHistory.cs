namespace price_management_api.Entities;

public class ItemPriceHistory : BaseEntity {
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = null!;

    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public required string Currency { get; set; }
    public DateTime EffectiveDate { get; set; }
    public string? Action { get; set; } // Update, Create, Delete
    public string? Notes { get; set; }
}
