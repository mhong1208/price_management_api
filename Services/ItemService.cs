using Microsoft.EntityFrameworkCore;
using price_management_api.Data;
using price_management_api.DTOs;
using price_management_api.Entities;
using price_management_api.Interfaces;
using price_management_api.Enums;

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
            Category = i.Category,
            Status = i.Status
        });
    }

    public async Task<ItemDto?> GetItemByIdAsync(Guid id)
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
            Category = item.Category,
            Status = item.Status
        };
    }

    public async Task<ItemDto> CreateItemAsync(CreateItemDto dto)
    {
        if (dto.Category == null) throw new Exception("Category is required");
        if (dto.Unit == null) throw new Exception("Unit is required");
        if (dto.ItemName == null) throw new Exception("Item name is required");
        if(string.IsNullOrEmpty(dto.ItemCode)) {
            dto.ItemCode = await GenerateItemCode(dto.Category);
        }

        var item = new Item
        {
            ItemCode = dto.ItemCode,
            ItemName = dto.ItemName,
            Description = dto.Description,
            Unit = dto.Unit,
            Category = dto.Category,
            Status = dto.Status,
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
            Category = item.Category,
            Status = item.Status
        };
    }

    public async Task<ItemDto> UpdateItemAsync(Guid id, CreateItemDto dto)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null) throw new Exception("Item not found");
        if (dto.Category == null) throw new Exception("Category is required");
        if (dto.Unit == null) throw new Exception("Unit is required");
        if (dto.ItemName == null) throw new Exception("Item name is required");

        item.ItemName = dto.ItemName;
        item.Description = dto.Description;
        item.Unit = dto.Unit;
        item.Category = dto.Category;
        item.Status = dto.Status;
        
        await _context.SaveChangesAsync();
        
        return new ItemDto
        {
            Id = item.Id,
            ItemName = item.ItemName,
            Description = item.Description,
            Unit = item.Unit,
            Category = item.Category,
            Status = item.Status
        };
    }

    public async Task<ItemDto> DeleteItemAsync(Guid id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null) throw new Exception("Item not found");
        
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
        
        return new ItemDto
        {
            Id = item.Id,
            ItemCode = item.ItemCode,
            ItemName = item.ItemName,
            Description = item.Description,
            Unit = item.Unit,
            Category = item.Category,
            Status = item.Status
        };
    }

    private async Task<string> GenerateItemCode(string category)
    {
        var date = DateTime.Now.ToString("yyMMdd");
        var prefix = $"{category}{date}";
        var count = await _context.Items
            .Where(i => i.ItemCode.StartsWith(prefix))
            .CountAsync();
            
        return $"{prefix}{(count + 1).ToString("D3")}";
    }

    public async Task<PagedResult<ItemDto>> GetItemsPaginatedAsync(PaginationRequestDto request)
    {
        var query = _context.Items.AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchText))
        {
            query = query.Where(i => i.ItemName.Contains(request.SearchText) || 
                                     i.ItemCode.Contains(request.SearchText));
        }

        if (!string.IsNullOrEmpty(request.Category))
        {
            query = query.Where(i => i.Category == request.Category);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(i => i.Status == request.Status.Value);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync();

        var itemDtos = items.Select(i => new ItemDto
        {
            Id = i.Id,
            ItemCode = i.ItemCode,
            ItemName = i.ItemName,
            Description = i.Description,
            Unit = i.Unit,
            Category = i.Category,
            Status = i.Status
        }).ToList();

        return new PagedResult<ItemDto>
        {
            Items = itemDtos,
            TotalCount = totalCount
        };
    }
} 
