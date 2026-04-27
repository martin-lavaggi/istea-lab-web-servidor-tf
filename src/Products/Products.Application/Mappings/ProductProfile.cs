using AutoMapper;
using Products.Domain.Models.DTOs;
using Products.Domain.Models.Entities;

namespace Products.Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}
