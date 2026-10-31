using FluentValidation;
using OnlineShop.CatalogService.Application.Common.Interfaces;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Commands.UpdateProduct;
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(
        ICategoryRepository categoryRepository,
        IBrandRepository brandRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(300).WithMessage("Name must not exceed 300 characters");

        RuleFor(x => x.Description)
            .MaximumLength(4000).When(x => !string.IsNullOrEmpty(x.Description))
            .WithMessage("Description must not exceed 4000 characters");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required")
            .MustAsync(async (id, ct) => await categoryRepository.ExistsAsync(id, ct))
            .WithMessage("Category does not exist");

        RuleFor(x => x.BrandId)
            .NotEmpty().WithMessage("BrandId is required")
            .MustAsync(async (id, ct) => await brandRepository.ExistsAsync(id, ct))
            .WithMessage("Brand does not exist");
    }
}