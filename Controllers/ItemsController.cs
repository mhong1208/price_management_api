using Microsoft.AspNetCore.Mvc;
using price_management_api.DTOs;
using price_management_api.Interfaces;

namespace price_management_api.Controllers;

[ApiController]
[Route("api/items")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    // Injection service vào controller
    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
    {
        var items = await _itemService.GetAllItemsAsync();
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ItemDetailDto>> GetItem(Guid id)
    {
        var item = await _itemService.GetItemByIdAsync(id);
        if (item == null) return NotFound(new { message = "Không tìm thấy mặt hàng này" });
        
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItem([FromBody] CreateItemDto dto)
    {
        var newItem = await _itemService.CreateItemAsync(dto);
        
        return CreatedAtAction(nameof(GetItem), new { id = newItem.Id }, newItem);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ItemDto>> UpdateItem(Guid id, [FromBody] CreateItemDto dto)
    {
        var updatedItem = await _itemService.UpdateItemAsync(id, dto);
        
        return Ok(updatedItem);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ItemDto>> DeleteItem(Guid id)
    {
        var deletedItem = await _itemService.DeleteItemAsync(id);
        
        return Ok(deletedItem);
    }

    [HttpPost("pagination")]
    public async Task<ActionResult<PagedResult<ItemDto>>> GetItemsPaginated([FromBody] PaginationRequestDto request)
    {
        var result = await _itemService.GetItemsPaginatedAsync(request);
        return Ok(result);
    }
}
