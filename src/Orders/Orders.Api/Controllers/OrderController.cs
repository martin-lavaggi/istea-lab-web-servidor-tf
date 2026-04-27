using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Orders.Api.Dtos;
using Orders.Api.Services;
using Orders.Domain.Models.Aggregates;
using Orders.Domain.Models.DTOs;
using Orders.Domain.Repositories;

namespace Orders.Api.Controllers;

/// <summary>
/// Controller REST del microservicio Orders. Expone listado, consulta por id y creación de órdenes,
/// orquestando las llamadas HTTP a Customers y Products durante la creación.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<Order> _validator;
    private readonly CustomersApiClient _customersClient;
    private readonly ProductsApiClient _productsClient;

    public OrderController(
        IOrderRepository repository,
        IMapper mapper,
        IValidator<Order> validator,
        CustomersApiClient customersClient,
        ProductsApiClient productsClient)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
        _customersClient = customersClient;
        _productsClient = productsClient;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var orders = await _repository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        if (id <= 0) return BadRequest("Invalid order ID.");

        var order = await _repository.GetOrderByIdAsync(id);
        if (order == null) return NotFound();

        return Ok(_mapper.Map<OrderDto>(order));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
    {
        if (request == null) return BadRequest("Request body is null.");

        var customer = await _customersClient.GetByIdAsync(request.CustomerId);
        if (customer == null) return BadRequest("Customer not found.");

        var order = new Order
        {
            OrderDate = DateTime.UtcNow,
            CustomerId = customer.Id,
            CustomerName = customer.Name
        };

        foreach (var item in request.Items)
        {
            var product = await _productsClient.GetByIdAsync(item.ProductId);
            if (product == null) continue;

            var quantityToSell = Math.Min(item.Quantity, product.Quantity);
            if (quantityToSell <= 0) continue;

            await _productsClient.UpdateAsync(product.Id, product with { Quantity = product.Quantity - quantityToSell });

            order.AddItem(product.Id, product.Name, product.Price, quantityToSell);
        }

        var validationResult = await _validator.ValidateAsync(order);
        if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

        var created = await _repository.AddOrderAsync(order);
        return CreatedAtAction(nameof(GetOrderById), new { id = created.Id }, _mapper.Map<OrderDto>(created));
    }
}
