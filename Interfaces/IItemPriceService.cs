using price_management_api.DTOs;

namespace price_management_api.Interfaces;

public interface IItemPriceService
{
    Task<IEnumerable<ItemPriceDto>> GetAllItemPricesAsync();
    Task<ItemPriceDto?> GetItemPriceByIdAsync(Guid id);
    Task<ItemPriceDto> CreateItemPriceAsync(CreateItemPriceDto dto);
    Task<ItemPriceDto> UpdateItemPriceAsync(Guid id, CreateItemPriceDto dto);
    Task<ItemPriceDto> DeleteItemPriceAsync(Guid id);
    Task<PagedResult<ItemPriceDto>> GetItemPricesPaginatedAsync(PaginationRequestDto request);
    Task<IEnumerable<ItemPriceHistoryDto>> GetPriceHistoryAsync(Guid? itemId, Guid? supplierId);
}
