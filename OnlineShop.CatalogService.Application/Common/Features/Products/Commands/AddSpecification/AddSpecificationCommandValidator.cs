using FluentValidation;
using OnlineShop.CatalogService.Application.Common.Interfaces;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.AddSpecification;
public class AddSpecificationCommandValidator : AbstractValidator<AddSpecificationCommand>
{
    public AddSpecificationCommandValidator(IProductRepository productRepository)
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required")
            .MustAsync(async (id, ct) => await productRepository.GetByIdAsync(id, ct) is not null)
            .WithMessage("Product does not exist");

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