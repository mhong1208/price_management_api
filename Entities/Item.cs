namespace price_management_api.Entities;

public class Item : BaseEntity {
    public required string ItemCode { get; set; }
    public required string ItemName { get; set; }
    public string? Description { get; set; }
    public string? Unit { get; set; }
    public string Status { get; set; } = "Active";

    // Mối quan hệ: Một mặt hàng có nhiều mức giá từ các NCC khác nhau
    public ICollection<ItemPrice> ItemPrices { get; set; } = [];
}