using AutoMapper;
using Products.Api.Dtos;
using Products.Domain.Models.Entities;

namespace Products.Api.Mappings;

/// <summary>
/// Mapeo entre ProductRequest (DTO de entrada) y la entidad Product.
/// </summary>
public class ProductRequestProfile : Profile
{
    public ProductRequestProfile()
    {
        CreateMap<ProductRequest, Product>().ReverseMap();
    }
}
