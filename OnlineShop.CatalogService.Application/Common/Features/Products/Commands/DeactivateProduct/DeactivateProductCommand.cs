using MediatR;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.DeactivateProduct;
public record DeactivateProductCommand(Guid Id) : IRequest;