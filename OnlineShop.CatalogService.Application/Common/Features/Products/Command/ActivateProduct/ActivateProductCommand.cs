using MediatR;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Command.ActivateProduct;
public record ActivateProductCommand(Guid Id) : IRequest;