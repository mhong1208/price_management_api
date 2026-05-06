using Microsoft.EntityFrameworkCore;
using price_management_api.Data;
using price_management_api.DTOs;
using price_management_api.Entities;
using price_management_api.Interfaces;
using price_management_api.Enums;

namespace price_management_api.Services;

public class SupplierService : ISupplierService
{
    private readonly ApplicationDbContext _context;

    public SupplierService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
    {
        var suppliers = await _context.Suppliers.ToListAsync();
        return suppliers.Select(MapToDto);
    }

    public async Task<SupplierDto?> GetSupplierByIdAsync(Guid id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        return supplier is null ? null : MapToDto(supplier);
    }

    public async Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.SupplierName))
            throw new Exception("Supplier name is required");

        if (string.IsNullOrWhiteSpace(dto.SupplierCode))
        {
            dto.SupplierCode = await GenerateSupplierCode();
        }

        var supplier = new Supplier
        {
            SupplierCode = dto.SupplierCode,
            SupplierName = dto.SupplierName,
            ContactPerson = dto.ContactPerson,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address,
            TaxCode = dto.TaxCode,
            Description = dto.Description,
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow
        };

        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();

        return MapToDto(supplier);
    }

    public async Task<SupplierDto> UpdateSupplierAsync(Guid id, CreateSupplierDto dto)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null) throw new Exception("Supplier not found");
        
        if (string.IsNullOrWhiteSpace(dto.SupplierName))
            throw new Exception("Supplier name is required");

        supplier.SupplierName = dto.SupplierName;
        if (!string.IsNullOrWhiteSpace(dto.SupplierCode))
        {
            supplier.SupplierCode = dto.SupplierCode;
        }
        supplier.ContactPerson = dto.ContactPerson;
        supplier.Email = dto.Email;
        supplier.Phone = dto.Phone;
        supplier.Address = dto.Address;
        supplier.TaxCode = dto.TaxCode;
        supplier.Description = dto.Description;
        supplier.Status = dto.Status;
        supplier.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToDto(supplier);
    }

    public async Task<SupplierDto> DeleteSupplierAsync(Guid id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null) throw new Exception("Supplier not found");

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();

        return MapToDto(supplier);
    }

    public async Task<PagedResult<SupplierDto>> GetSuppliersPaginatedAsync(PaginationRequestDto request)
    {
        var query = _context.Suppliers.AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchText))
        {
            query = query.Where(s => s.SupplierName.Contains(request.SearchText) ||
                                     s.SupplierCode.Contains(request.SearchText) ||
                                     s.Description.Contains(request.SearchText)) ;
        }

        if (request.Status.HasValue)
        {
            query = query.Where(s => s.Status == request.Status.Value);
        }

        var totalCount = await query.CountAsync();
        var suppliers = await query.Skip(request.Skip).Take(request.Take).ToListAsync();

        return new PagedResult<SupplierDto>
        {
            Items = suppliers.Select(MapToDto).ToList(),
            TotalCount = totalCount
        };
    }

    private async Task<string> GenerateSupplierCode()
    {
        var date = DateTime.UtcNow.ToString("yyMMdd");
        var prefix = $"SUP{date}";
        var count = await _context.Suppliers
            .Where(s => s.SupplierCode.StartsWith(prefix))
            .CountAsync();

        return $"{prefix}{(count + 1):D3}";
    }

    private static SupplierDto MapToDto(Supplier supplier)
    {
        return new SupplierDto
        {
            Id = supplier.Id,
            SupplierCode = supplier.SupplierCode,
            SupplierName = supplier.SupplierName,
            ContactPerson = supplier.ContactPerson,
            Email = supplier.Email,
            Phone = supplier.Phone,
            Address = supplier.Address,
            TaxCode = supplier.TaxCode,
            Description = supplier.Description,
            Status = supplier.Status
        };
    }
}
