using price_management_api.Enums;

namespace price_management_api.DTOs;

public class SupplierDto
{
    public Guid Id { get; set; }
    public string SupplierCode { get; set; } = string.Empty;
    public string SupplierName { get; set; } = string.Empty;
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? TaxCode { get; set; }
    public string? Description { get; set; }
    public ItemStatus Status { get; set; } = ItemStatus.ACTIVE;
}

public class CreateSupplierDto
{
    public string? SupplierCode { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? TaxCode { get; set; }
    public string? Description { get; set; }
    public ItemStatus Status { get; set; } = ItemStatus.ACTIVE;
}
