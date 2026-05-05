using price_management_api.DTOs;

namespace price_management_api.Interfaces;

public interface IItemService
{
    Task<IEnumerable<ItemDto>> GetAllItemsAsync();
    Task<ItemDto?> GetItemByIdAsync(Guid id);
    Task<ItemDto> CreateItemAsync(CreateItemDto dto);
    Task<ItemDto> UpdateItemAsync(Guid id, CreateItemDto dto);
    Task<ItemDto> DeleteItemAsync(Guid id);
    Task<PagedResult<ItemDto>> GetItemsPaginatedAsync(PaginationRequestDto request);
}
