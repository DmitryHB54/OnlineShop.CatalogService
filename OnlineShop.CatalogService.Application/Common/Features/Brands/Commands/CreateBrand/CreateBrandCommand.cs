using MediatR;

namespace OnlineShop.CatalogService.Application.Common.Features.Brands.Commands.CreateBrand;

public record CreateBrandCommand(
    string Name,
    string Slug,
    string? LogoUrl = null) : IRequest<Guid>;