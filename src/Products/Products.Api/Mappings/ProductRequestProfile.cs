using AutoMapper;
using Products.Api.Dtos;
using Products.Domain.Models.Entities;

namespace Products.Api.Mappings;

public class ProductRequestProfile : Profile
{
    public ProductRequestProfile()
    {
        CreateMap<ProductRequest, Product>().ReverseMap();
    }
}
