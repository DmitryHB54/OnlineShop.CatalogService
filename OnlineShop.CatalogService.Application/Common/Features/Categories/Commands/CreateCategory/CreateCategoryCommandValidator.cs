using FluentValidation;

namespace OnlineShop.CatalogService.Application.Common.Features.Categories.Commands.CreateCategory;
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug is required")
            .MaximumLength(200)
            .Matches("^[a-z0-9-]+$")
            .WithMessage("Slug must contain only lowercase letters, digits, and hyphens");
    }
}