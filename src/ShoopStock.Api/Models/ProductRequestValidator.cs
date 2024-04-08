using FluentValidation;
using ShoopStock.Core.Models.Requests;

namespace ShoopStock.Api.Models;

public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Code)
       .NotEmpty()
       .NotNull();

        RuleFor(x => x.Price)
            .NotNull()
            .NotEmpty();

    }
}
