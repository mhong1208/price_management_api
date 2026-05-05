using price_management_api.DTOs;

namespace price_management_api.Interfaces;

public interface ISupplierService
{
    Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync();
    Task<SupplierDto?> GetSupplierByIdAsync(Guid id);
    Task<SupplierDto> CreateSupplierAsync(CreateSupplierDto dto);
    Task<SupplierDto> UpdateSupplierAsync(Guid id, CreateSupplierDto dto);
    Task<SupplierDto> DeleteSupplierAsync(Guid id);
    Task<PagedResult<SupplierDto>> GetSuppliersPaginatedAsync(PaginationRequestDto request);
}
