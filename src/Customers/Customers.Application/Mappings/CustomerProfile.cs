using AutoMapper;
using Customers.Domain.Models.DTOs;
using Customers.Domain.Models.Entities;

namespace Customers.Application.Mappings;

/// <summary>
/// Mapeo bidireccional entre la entidad Customer y el DTO de salida CustomerDto.
/// </summary>
public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();
    }
}
