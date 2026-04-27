using AutoMapper;
using Customers.Api.Dtos;
using Customers.Domain.Models.Entities;

namespace Customers.Api.Mappings;

/// <summary>
/// Mapeo entre CustomerRequest (DTO de entrada) y la entidad Customer.
/// </summary>
public class CustomerRequestProfile : Profile
{
    public CustomerRequestProfile()
    {
        CreateMap<CustomerRequest, Customer>().ReverseMap();
    }
}
