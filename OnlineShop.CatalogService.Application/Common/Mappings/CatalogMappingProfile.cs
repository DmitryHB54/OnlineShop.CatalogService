using AutoMapper;
using OnlineShop.CatalogService.Application.Common.Features.Categories.Dtos;
using OnlineShop.CatalogService.Application.Common.Features.Products.Dtos;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Mappings;
public class CatalogMappingProfile : Profile
{
    public CatalogMappingProfile()
    {
        CreateMap<Category, CategoryDto>();

        CreateMap<Product, ProductDto>()
            .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
            .ForMember(d => d.BrandName, o => o.MapFrom(s => s.Brand.Name));

        CreateMap<Product, ProductDetailsDto>()
            .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.Name))
            .ForMember(d => d.BrandName, o => o.MapFrom(s => s.Brand.Name))
            .ForMember(d => d.Specifications, o => o.MapFrom(s =>
                s.Specifications.OrderBy(x => x.DisplayOrder)));

        CreateMap<ProductSpecification, SpecificationDto>();
    }
}