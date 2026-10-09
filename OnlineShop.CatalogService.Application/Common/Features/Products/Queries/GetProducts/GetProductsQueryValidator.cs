using FluentValidation;

namespace OnlineShop.CatalogService.Application.Common.Features.Products.Queries.GetProducts;
public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page must be at least 1");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100");

        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0).When(x => x.MinPrice.HasValue)
            .WithMessage("MinPrice cannot be negative");

        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(0).When(x => x.MaxPrice.HasValue)
            .WithMessage("MaxPrice cannot be negative");

        RuleFor(x => x)
            .Must(x => !x.MinPrice.HasValue || !x.MaxPrice.HasValue || x.MinPrice <= x.MaxPrice)
            .WithMessage("MinPrice must be less than or equal to MaxPrice");

        RuleFor(x => x.Search)
            .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Search))
            .WithMessage("Search query must not exceed 200 characters");
    }
}