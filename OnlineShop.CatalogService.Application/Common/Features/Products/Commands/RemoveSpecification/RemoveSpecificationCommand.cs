using MediatR;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.RemoveSpecification;

public record RemoveSpecificationCommand(
    Guid ProductId,
    Guid SpecificationId) : IRequest;