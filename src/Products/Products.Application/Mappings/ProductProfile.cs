using AutoMapper;
using Products.Domain.Models.DTOs;
using Products.Domain.Models.Entities;

namespace Products.Application.Mappings;

/// <summary>
/// Mapeo bidireccional entre la entidad Product y el DTO de salida ProductDto.
/// </summary>
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}
