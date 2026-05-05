namespace price_management_api.Entities;

public class ItemPrice : BaseEntity {
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;

    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public decimal Price { get; set; }
    public required string Currency { get; set; }
    public DateTime EffectiveDate { get; set; }
}