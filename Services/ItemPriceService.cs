using Microsoft.EntityFrameworkCore;
using price_management_api.Data;
using price_management_api.DTOs;
using price_management_api.Entities;
using price_management_api.Interfaces;

namespace price_management_api.Services;

public class ItemPriceService : IItemPriceService
{
    private readonly ApplicationDbContext _context;

    public ItemPriceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ItemPriceDto>> GetAllItemPricesAsync()
    {
        var itemPrices = await _context.ItemPrices
            .Include(ip => ip.Item)
            .Include(ip => ip.Supplier)
            .ToListAsync();

        return itemPrices.Select(MapToDto);
    }

    public async Task<ItemPriceDto?> GetItemPriceByIdAsync(Guid id)
    {
        var itemPrice = await _context.ItemPrices
            .Include(ip => ip.Item)
            .Include(ip => ip.Supplier)
            .FirstOrDefaultAsync(ip => ip.Id == id);

        return itemPrice is null ? null : MapToDto(itemPrice);
    }

    public async Task<ItemPriceDto> CreateItemPriceAsync(CreateItemPriceDto dto)
    {
        var itemId = ParseGuid(dto.ItemId, nameof(dto.ItemId));
        var supplierId = ParseGuid(dto.SupplierId, nameof(dto.SupplierId));
        var effectiveDate = ParseDateTime(dto.EffectiveDate, nameof(dto.EffectiveDate));

        var item = await _context.Items.FindAsync(itemId);
        if (item == null) throw new Exception("Item not found");

        var supplier = await _context.Suppliers.FindAsync(supplierId);
        if (supplier == null) throw new Exception("Supplier not found");

        var itemPrice = new ItemPrice
        {
            ItemId = itemId,
            SupplierId = supplierId,
            Price = dto.Price,
            Currency = dto.Currency,
            EffectiveDate = effectiveDate,
            Notes = dto.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.ItemPrices.Add(itemPrice);

        var history = new ItemPriceHistory
        {
            ItemId = itemId,
            SupplierId = supplierId,
            OldPrice = 0,
            NewPrice = dto.Price,
            Currency = dto.Currency,
            EffectiveDate = effectiveDate,
            Action = "CREATE",
            Notes = dto.Notes,
            CreatedAt = DateTime.UtcNow
        };
        _context.ItemPriceHistories.Add(history);

        await _context.SaveChangesAsync();

        return MapToDto(await LoadIncludesAsync(itemPrice.Id));
    }

    public async Task<ItemPriceDto> UpdateItemPriceAsync(Guid id, CreateItemPriceDto dto)
    {
        var itemPrice = await _context.ItemPrices.FindAsync(id);
        if (itemPrice == null) throw new Exception("Item price record not found");

        var oldPrice = itemPrice.Price;
        itemPrice.ItemId = ParseGuid(dto.ItemId, nameof(dto.ItemId));
        itemPrice.SupplierId = ParseGuid(dto.SupplierId, nameof(dto.SupplierId));
        itemPrice.Price = dto.Price;
        itemPrice.Currency = dto.Currency;
        itemPrice.EffectiveDate = ParseDateTime(dto.EffectiveDate, nameof(dto.EffectiveDate));
        itemPrice.Notes = dto.Notes;
        itemPrice.UpdatedAt = DateTime.UtcNow;

        var history = new ItemPriceHistory
        {
            ItemId = itemPrice.ItemId,
            SupplierId = itemPrice.SupplierId,
            OldPrice = oldPrice,
            NewPrice = itemPrice.Price,
            Currency = itemPrice.Currency,
            EffectiveDate = itemPrice.EffectiveDate,
            Action = "UPDATE",
            Notes = itemPrice.Notes,
            CreatedAt = DateTime.UtcNow
        };
        _context.ItemPriceHistories.Add(history);

        await _context.SaveChangesAsync();

        return MapToDto(await LoadIncludesAsync(itemPrice.Id));
    }

    public async Task<ItemPriceDto> DeleteItemPriceAsync(Guid id)
    {
        var itemPrice = await _context.ItemPrices.FindAsync(id);
        if (itemPrice == null) throw new Exception("Item price record not found");

        var history = new ItemPriceHistory
        {
            ItemId = itemPrice.ItemId,
            SupplierId = itemPrice.SupplierId,
            OldPrice = itemPrice.Price,
            NewPrice = 0,
            Currency = itemPrice.Currency,
            EffectiveDate = itemPrice.EffectiveDate,
            Action = "DELETE",
            Notes = "Record deleted",
            CreatedAt = DateTime.UtcNow
        };
        _context.ItemPriceHistories.Add(history);

        _context.ItemPrices.Remove(itemPrice);
        await _context.SaveChangesAsync();

        return MapToDto(itemPrice);
    }

    public async Task<PagedResult<ItemPriceDto>> GetItemPricesPaginatedAsync(PaginationRequestDto request)
    {
        var query = _context.ItemPrices
            .Include(ip => ip.Item)
            .Include(ip => ip.Supplier)
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchText))
        {
            query = query.Where(ip => ip.Currency.Contains(request.SearchText) ||
                                      ip.Notes.Contains(request.SearchText) ||
                                      ip.Item.ItemName.Contains(request.SearchText) ||
                                      ip.Item.ItemCode.Contains(request.SearchText) ||
                                      ip.Supplier.SupplierName.Contains(request.SearchText) ||
                                      ip.Supplier.SupplierCode.Contains(request.SearchText));
        }
        
        if (request.ItemId.HasValue)
        {
            query = query.Where(ip => ip.ItemId == request.ItemId.Value);
        }

        if (request.SupplierId.HasValue)
        {
            query = query.Where(ip => ip.SupplierId == request.SupplierId.Value);
        }

        var totalCount = await query.CountAsync();
        var itemPrices = await query.Skip(request.Skip).Take(request.Take).ToListAsync();

        return new PagedResult<ItemPriceDto>
        {
            Items = itemPrices.Select(MapToDto).ToList(),
            TotalCount = totalCount
        };
    }

    private async Task<ItemPrice> LoadIncludesAsync(Guid id)
    {
        var itemPrice = await _context.ItemPrices
            .Include(ip => ip.Item)
            .Include(ip => ip.Supplier)
            .FirstAsync(ip => ip.Id == id);

        return itemPrice;
    }

    private static Guid ParseGuid(string value, string name)
    {
        if (!Guid.TryParse(value, out var guid))
            throw new Exception($"{name} is not a valid GUID");

        return guid;
    }

    private static DateTime ParseDateTime(string value, string name)
    {
        if (!DateTime.TryParse(value, out var dateTime))
            throw new Exception($"{name} is not a valid date/time");

        return dateTime;
    }

    private static ItemPriceDto MapToDto(ItemPrice itemPrice)
    {
        return new ItemPriceDto
        {
            Id = itemPrice.Id.ToString(),
            ItemId = itemPrice.ItemId.ToString(),
            Item = itemPrice.Item is null ? null : new ItemDto
            {
                Id = itemPrice.Item.Id,
                ItemCode = itemPrice.Item.ItemCode,
                ItemName = itemPrice.Item.ItemName,
                Description = itemPrice.Item.Description,
                Unit = itemPrice.Item.Unit,
                Category = itemPrice.Item.Category,
                Status = itemPrice.Item.Status
            },
            SupplierId = itemPrice.SupplierId.ToString(),
            Supplier = itemPrice.Supplier is null ? null : new SupplierDto
            {
                Id = itemPrice.Supplier.Id,
                SupplierCode = itemPrice.Supplier.SupplierCode,
                SupplierName = itemPrice.Supplier.SupplierName,
                ContactPerson = itemPrice.Supplier.ContactPerson,
                Email = itemPrice.Supplier.Email,
                Phone = itemPrice.Supplier.Phone,
                Address = itemPrice.Supplier.Address,
                TaxCode = itemPrice.Supplier.TaxCode,
                Description = itemPrice.Supplier.Description,
                Status = itemPrice.Supplier.Status
            },
            Price = itemPrice.Price,
            Currency = itemPrice.Currency,
            EffectiveDate = itemPrice.EffectiveDate.ToString("o"),
            Notes = itemPrice.Notes,
            CreatedAt = itemPrice.CreatedAt.ToString("o"),
            UpdatedAt = itemPrice.UpdatedAt?.ToString("o")
        };
    }

    public async Task<PagedResult<ItemPriceHistoryDto>> GetPriceHistoryAsync(PaginationRequestDto request)
    {
        var query = _context.ItemPriceHistories
            .Include(iph => iph.Item)
            .Include(iph => iph.Supplier)
            .AsQueryable();

        if (request.ItemId.HasValue)
        {
            query = query.Where(iph => iph.ItemId == request.ItemId.Value);
        }

        if (request.SupplierId.HasValue)
        {
            query = query.Where(iph => iph.SupplierId == request.SupplierId.Value);
        }

        if (!string.IsNullOrEmpty(request.SearchText))
        {
            query = query.Where(iph => iph.Item.ItemName.Contains(request.SearchText) ||
                                       iph.Item.ItemCode.Contains(request.SearchText) ||
                                       iph.Supplier.SupplierName.Contains(request.SearchText) ||
                                       iph.Supplier.SupplierCode.Contains(request.SearchText));
        }

        var totalCount = await query.CountAsync();

        var history = await query
            .OrderByDescending(iph => iph.CreatedAt)
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync();

        var dtos = history.Select(h => new ItemPriceHistoryDto
        {
            Id = h.Id,
            ItemId = h.ItemId,
            ItemName = h.Item?.ItemName ?? string.Empty,
            SupplierId = h.SupplierId,
            SupplierName = h.Supplier?.SupplierName ?? string.Empty,
            OldPrice = h.OldPrice,
            NewPrice = h.NewPrice,
            Currency = h.Currency,
            EffectiveDate = h.EffectiveDate.ToString("yyyy-MM-dd"),
            Action = h.Action,
            Notes = h.Notes,
            CreatedAt = h.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
        }).ToList();

        return new PagedResult<ItemPriceHistoryDto>
        {
            Items = dtos,
            TotalCount = totalCount
        };
    }
}
