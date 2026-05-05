using System.Collections.Generic;
using price_management_api.Enums;

namespace price_management_api.Entities;

public class Item : BaseEntity {
    public required string ItemCode { get; set; }
    public required string ItemName { get; set; }
    public string? Description { get; set; }
    public string? Unit { get; set; }
    public string? Category { get; set; }
    public ItemStatus Status { get; set; } = ItemStatus.ACTIVE;
    // relationship with item prices from different suppliers
    public ICollection<ItemPrice> ItemPrices { get; set; } = [];
}