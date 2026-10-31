using MediatR;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.AddSpecification;

public record AddSpecificationCommand(
    Guid ProductId,
    string Key,
    string Value,
    int DisplayOrder = 0) : IRequest<Guid>;