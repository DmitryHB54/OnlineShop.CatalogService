using MediatR;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.UpdateSpecification;

public record UpdateSpecificationCommand(
    Guid ProductId,
    Guid SpecificationId,
    string Key,
    string Value,
    int DisplayOrder = 0) : IRequest;