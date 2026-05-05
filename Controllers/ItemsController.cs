using Microsoft.AspNetCore.Mvc;
using price_management_api.DTOs;
using price_management_api.Interfaces;

namespace price_management_api.Controllers;

[ApiController]
[Route("api/[controller]")] // Route sẽ là: /api/items
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

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetItem(int id)
    {
        var item = await _itemService.GetItemByIdAsync(id);
        if (item == null) return NotFound(new { message = "Không tìm thấy mặt hàng này" });
        
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItem([FromBody] CreateItemDto dto)
    {
        var newItem = await _itemService.CreateItemAsync(dto);
        
        // Trả về HTTP 201 Created cùng với URL để xem lại dữ liệu vừa tạo
        return CreatedAtAction(nameof(GetItem), new { id = newItem.Id }, newItem);
    }
}
