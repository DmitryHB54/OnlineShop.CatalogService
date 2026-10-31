using FluentValidation;
using OnlineShop.CatalogService.Application.Common.Features.Products.Commands.UpdateSpecification;
using OnlineShop.CatalogService.Application.Common.Interfaces;
using OnlineShop.CatalogService.Domain.Entities;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.UpdateSpecificationl;
public class UpdateSpecificationCommandValidator : AbstractValidator<UpdateSpecificationCommand>
{
    public UpdateSpecificationCommandValidator(IProductRepository productRepository)
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .MustAsync(async (id, ct) =>
            {
                Product? product = await productRepository.GetByIdAsync(id, ct);
                return product is not null;
                // await productRepository.GetByIdAsync(id, ct) is not null
            })
            .WithMessage("Product does not exist");

        RuleFor(x => x.SpecificationId)
            .NotEmpty().WithMessage("SpecificationId is required");

        RuleFor(x => x.Key)
            .NotEmpty().WithMessage("Key is required")
            .MaximumLength(200).WithMessage("Key must not exceed 200 characters");

        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Value is required")
            .MaximumLength(1000).WithMessage("Value must not exceed 1000 characters");

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0).WithMessage("DisplayOrder cannot be negative");
    }
}