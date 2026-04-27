using AutoMapper;
using Customers.Api.Dtos;
using Customers.Domain.Models.DTOs;
using Customers.Domain.Models.Entities;
using Customers.Domain.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Api.Controllers;

/// <summary>
/// Controller REST del microservicio Customers. Expone los 5 endpoints CRUD.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<Customer> _validator;

    public CustomerController(
        ICustomerRepository repository,
        IMapper mapper,
        IValidator<Customer> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var customers = await _repository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        if (id <= 0) return BadRequest("Invalid customer ID.");

        var customer = await _repository.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound();

        return Ok(_mapper.Map<CustomerDto>(customer));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerRequest request)
    {
        if (request == null) return BadRequest("Request body is null.");

        var customer = _mapper.Map<Customer>(request);
        customer.RegistrationDate = DateTime.UtcNow;

        var validationResult = await _validator.ValidateAsync(customer);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var created = await _repository.AddCustomerAsync(customer);
        return CreatedAtAction(nameof(GetCustomerById), new { id = created.Id }, _mapper.Map<CustomerDto>(created));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerRequest request)
    {
        if (id <= 0) return BadRequest("Invalid customer ID.");
        if (request == null) return BadRequest("Request body is null.");

        var customer = _mapper.Map<Customer>(request);

        var validationResult = await _validator.ValidateAsync(customer);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var updated = await _repository.UpdateCustomerAsync(id, customer);
        if (updated == null) return NotFound();

        return Ok(_mapper.Map<CustomerDto>(updated));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        if (id <= 0) return BadRequest("Invalid customer ID.");

        var deleted = await _repository.DeleteCustomerAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
