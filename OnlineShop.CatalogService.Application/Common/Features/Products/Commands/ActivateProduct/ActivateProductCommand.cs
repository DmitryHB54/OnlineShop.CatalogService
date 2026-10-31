using MediatR;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.ActivateProduct;
public record ActivateProductCommand(Guid Id) : IRequest;