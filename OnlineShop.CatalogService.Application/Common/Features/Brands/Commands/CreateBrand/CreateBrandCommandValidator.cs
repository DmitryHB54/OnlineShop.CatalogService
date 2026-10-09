using FluentValidation;
using OnlineShop.CatalogService.Application.Common.Interfaces;

namespace OnlineShop.CatalogService.Application.Common.Features.Brands.Commands.CreateBrand;
public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator(IBrandRepository brandRepository)
    {
        RuleFor(b => b.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug is required")
            .MaximumLength(200).WithMessage("SLug must not exceed 200 characters")
            .Matches("^[a-z0-9-]+$")
            .WithMessage("Slug must contain only lowercase letters, digits, and hyphens")
            .MustAsync(async (slug, ct) =>
                !await brandRepository.ExistsWithSlugAsync(slug, ct))
            .WithMessage("Brand with this slug already exists");

        RuleFor(x => x.LogoUrl)
            .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.LogoUrl))
            .WithMessage("LogoUrl must not exceed 500 characters");
    }
}