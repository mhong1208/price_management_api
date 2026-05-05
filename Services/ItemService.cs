using Microsoft.EntityFrameworkCore;
using price_management_api.Data;
using price_management_api.DTOs;
using price_management_api.Entities;
using price_management_api.Interfaces;

namespace price_management_api.Services;

public class ItemService : IItemService
{
    private readonly ApplicationDbContext _context;

    public ItemService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ItemDto>> GetAllItemsAsync()
    {
        var items = await _context.Items.ToListAsync();
        return items.Select(i => new ItemDto
        {
            Id = i.Id,
            ItemCode = i.ItemCode,
            ItemName = i.ItemName,
            Description = i.Description,
            Unit = i.Unit,
            Status = i.Status
        });
    }

    public async Task<ItemDto?> GetItemByIdAsync(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null) return null;
        
        return new ItemDto
        {
            Id = item.Id,
            ItemCode = item.ItemCode,
            ItemName = item.ItemName,
            Description = item.Description,
            Unit = item.Unit,
            Status = item.Status
        };
    }

    public async Task<ItemDto> CreateItemAsync(CreateItemDto dto)
    {
        var item = new Item
        {
            ItemCode = dto.ItemCode,
            ItemName = dto.ItemName,
            Description = dto.Description,
            Unit = dto.Unit,
            Status = "Active", // Mặc định khi mới tạo
            CreatedAt = DateTime.UtcNow
        };

        _context.Items.Add(item);
        await _context.SaveChangesAsync();

        return new ItemDto
        {
            Id = item.Id,
            ItemCode = item.ItemCode,
            ItemName = item.ItemName,
            Description = item.Description,
            Unit = item.Unit,
            Status = item.Status
        };
    }
}
