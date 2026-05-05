using Microsoft.AspNetCore.Mvc;
using price_management_api.DTOs;
using price_management_api.Interfaces;

namespace price_management_api.Controllers;

[ApiController]
[Route("api/suppliers")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> GetSuppliers()
    {
        var suppliers = await _supplierService.GetAllSuppliersAsync();
        return Ok(suppliers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierDto>> GetSupplier(Guid id)
    {
        var supplier = await _supplierService.GetSupplierByIdAsync(id);
        if (supplier == null) return NotFound(new { message = "Không tìm thấy nhà cung cấp này" });

        return Ok(supplier);
    }

    [HttpPost]
    public async Task<ActionResult<SupplierDto>> CreateSupplier([FromBody] CreateSupplierDto dto)
    {
        var newSupplier = await _supplierService.CreateSupplierAsync(dto);
        return CreatedAtAction(nameof(GetSupplier), new { id = newSupplier.Id }, newSupplier);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SupplierDto>> UpdateSupplier(Guid id, [FromBody] CreateSupplierDto dto)
    {
        var updatedSupplier = await _supplierService.UpdateSupplierAsync(id, dto);
        return Ok(updatedSupplier);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<SupplierDto>> DeleteSupplier(Guid id)
    {
        var deletedSupplier = await _supplierService.DeleteSupplierAsync(id);
        return Ok(deletedSupplier);
    }

    [HttpPost("pagination")]
    public async Task<ActionResult<PagedResult<SupplierDto>>> GetSuppliersPaginated([FromBody] PaginationRequestDto request)
    {
        var result = await _supplierService.GetSuppliersPaginatedAsync(request);
        return Ok(result);
    }
}
