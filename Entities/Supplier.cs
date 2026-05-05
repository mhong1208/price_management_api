namespace price_management_api.Entities;

public class Supplier : BaseEntity {
    public string SupplierCode { get; set; }
    public string SupplierName { get; set; }
    public string Email { get; set; }
    // ... các trường khác theo đề bài
    
    public ICollection<ItemPrice> ItemPrices { get; set; }
}