using Microsoft.AspNetCore.Mvc;
using price_management_api.DTOs;
using price_management_api.Interfaces;

namespace price_management_api.Controllers;

[ApiController]
[Route("api/item-prices")]
public class ItemPricesController : ControllerBase
{
    private readonly IItemPriceService _itemPriceService;

    public ItemPricesController(IItemPriceService itemPriceService)
    {
        _itemPriceService = itemPriceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemPriceDto>>> GetItemPrices()
    {
        var itemPrices = await _itemPriceService.GetAllItemPricesAsync();
        return Ok(itemPrices);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemPriceDto>> GetItemPrice(Guid id)
    {
        var itemPrice = await _itemPriceService.GetItemPriceByIdAsync(id);
        if (itemPrice == null) return NotFound(new { message = "Không tìm thấy giá mặt hàng này" });

        return Ok(itemPrice);
    }

    [HttpPost]
    public async Task<ActionResult<ItemPriceDto>> CreateItemPrice([FromBody] CreateItemPriceDto dto)
    {
        var newItemPrice = await _itemPriceService.CreateItemPriceAsync(dto);
        return CreatedAtAction(nameof(GetItemPrice), new { id = newItemPrice.Id }, newItemPrice);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ItemPriceDto>> UpdateItemPrice(Guid id, [FromBody] CreateItemPriceDto dto)
    {
        var updatedItemPrice = await _itemPriceService.UpdateItemPriceAsync(id, dto);
        return Ok(updatedItemPrice);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ItemPriceDto>> DeleteItemPrice(Guid id)
    {
        var deletedItemPrice = await _itemPriceService.DeleteItemPriceAsync(id);
        return Ok(deletedItemPrice);
    }

    [HttpPost("pagination")]
    public async Task<ActionResult<PagedResult<ItemPriceDto>>> GetItemPricesPaginated([FromBody] PaginationRequestDto request)
    {
        var result = await _itemPriceService.GetItemPricesPaginatedAsync(request);
        return Ok(result);
    }

    [HttpGet("history")]
    public async Task<ActionResult<PagedResult<ItemPriceHistoryDto>>> GetPriceHistory([FromQuery] PaginationRequestDto request)
    {
        var result = await _itemPriceService.GetPriceHistoryAsync(request);
        return Ok(result);
    }
}
