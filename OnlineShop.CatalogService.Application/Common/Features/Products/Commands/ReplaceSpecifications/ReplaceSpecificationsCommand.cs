using MediatR;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.ReplaceSpecifications;
public record ReplaceSpecificationsCommand(
    Guid ProductId,
    List<SpecificationInput> Specifications) : IRequest;

public record SpecificationInput(string Key, string Value, int DisplayOrder = 0);