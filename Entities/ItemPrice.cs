namespace price_management_api.Entities;

public class ItemPrice : BaseEntity {
    public int ItemId { get; set; }
    public Item Item { get; set; }
    
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public DateTime EffectiveDate { get; set; }
}