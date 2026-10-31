using FluentValidation;
using OnlineShop.CatalogService.Application.Common.Interfaces;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.CreateProduct;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(
        ICategoryRepository categoryRepository,
        IBrandRepository brandRepository,
        IProductRepository productRepository)
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required")
           .MaximumLength(300).WithMessage("Name must not exceed 300 characters");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug is required")
            .MaximumLength(300).WithMessage("Slug must not exceed 300 characters")
            .Matches("^[a-z0-9-]+$")
            .WithMessage("SLug must contain only lowercase letters, digits, and hyphens")
            .MustAsync(async (slug, ct) => !await productRepository.ExistsWithSlugAsync(slug, ct))
            .WithMessage("Product with this slug already exists");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency is required")
            .Length(3).WithMessage("Currency must be 3 characters (ISO 4217)");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required")
            .MustAsync(async (id, ct) => await categoryRepository.ExistsAsync(id, ct))
            .WithMessage("Category does not exist");

        RuleFor(x => x.BrandId)
            .NotEmpty().WithMessage("BrandId is required")
            .MustAsync(async (id, ct) => await brandRepository.ExistsAsync(id, ct))
            .WithMessage("Brand does not exist");

        When(x => x.Specifications is not null, () =>
        {
            RuleForEach(x => x.Specifications!).ChildRules(spec =>
            {
                spec.RuleFor(s => s.Key)
                    .NotEmpty().WithMessage("Specification key is required")
                    .MaximumLength(200);

                spec.RuleFor(s => s.Value)
                    .NotEmpty().WithMessage("Specification value is required")
                    .MaximumLength(1000);
            });
        });
    }
}