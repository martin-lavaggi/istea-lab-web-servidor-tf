using FluentValidation;
using Products.Domain.Models.Entities;

namespace Products.Application.Validations;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name max length is 200");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be 0 or greater");

        RuleFor(p => p.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity must be 0 or greater");
    }
}
