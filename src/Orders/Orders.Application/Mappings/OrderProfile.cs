using AutoMapper;
using Orders.Domain.Models.Aggregates;
using Orders.Domain.Models.DTOs;

namespace Orders.Application.Mappings;

/// <summary>
/// Mapeo del agregado Order y sus OrderItem a los DTO de salida.
/// </summary>
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
    }
}
