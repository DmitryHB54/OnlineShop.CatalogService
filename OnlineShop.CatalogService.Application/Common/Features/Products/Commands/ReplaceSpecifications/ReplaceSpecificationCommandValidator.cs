using FluentValidation;
using OnlineShop.CatalogService.Application.Common.Interfaces;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.ReplaceSpecifications;
public class ReplaceSpecificationCommandValidator : AbstractValidator<ReplaceSpecificationsCommand>
{
    public ReplaceSpecificationCommandValidator(
        IProductRepository productRepository)
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .MustAsync(async (id, ct) =>
                await productRepository.GetByIdAsync(id, ct) is not null)
            .WithMessage("Product does not exist");

        RuleFor(x => x.Specifications)
            .NotNull().WithMessage("Specifications list is required");

        RuleForEach(x => x.Specifications).ChildRules(spec =>
        {
            spec.RuleFor(s => s.Key)
                .NotEmpty().WithMessage("Key is required")
                .MaximumLength(200);

            spec.RuleFor(s => s.Value)
                .NotEmpty().WithMessage("Value is required")
                .MaximumLength(1000);

            spec.RuleFor(s => s.DisplayOrder)
                .GreaterThanOrEqualTo(0);
        });

        RuleFor(x => x.Specifications)
            .Must(specs => specs.Select(s => s.Key.ToLower()).Distinct().Count() == specs.Count)
            .When(x => x.Specifications is { Count: > 0 })
            .WithMessage("Specifications keys must be unique");
    }
}