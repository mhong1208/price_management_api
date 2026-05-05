namespace price_management_api.Entities;

public abstract class BaseEntity
{
    // Cột Id chung cho mọi bảng
    public int Id { get; set; }

    // Các trường Audit (lưu vết)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
