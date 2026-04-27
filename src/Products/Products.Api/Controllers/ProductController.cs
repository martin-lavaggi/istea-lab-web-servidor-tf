using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Products.Api.Dtos;
using Products.Domain.Models.DTOs;
using Products.Domain.Models.Entities;
using Products.Domain.Repositories;

namespace Products.Api.Controllers;

/// <summary>
/// Controller REST del microservicio Products. Expone los 5 endpoints CRUD.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<Product> _validator;

    public ProductController(
        IProductRepository repository,
        IMapper mapper,
        IValidator<Product> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _repository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        if (id <= 0) return BadRequest("Invalid product ID.");

        var product = await _repository.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
    {
        if (request == null) return BadRequest("Request body is null.");

        var product = _mapper.Map<Product>(request);

        var validationResult = await _validator.ValidateAsync(product);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var created = await _repository.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = created.Id }, _mapper.Map<ProductDto>(created));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequest request)
    {
        if (id <= 0) return BadRequest("Invalid product ID.");
        if (request == null) return BadRequest("Request body is null.");

        var product = _mapper.Map<Product>(request);

        var validationResult = await _validator.ValidateAsync(product);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var updated = await _repository.UpdateProductAsync(id, product);
        if (updated == null) return NotFound();

        return Ok(_mapper.Map<ProductDto>(updated));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        if (id <= 0) return BadRequest("Invalid product ID.");

        var deleted = await _repository.DeleteProductAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
