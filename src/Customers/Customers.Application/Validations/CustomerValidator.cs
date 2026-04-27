using Customers.Domain.Models.Entities;
using FluentValidation;

namespace Customers.Application.Validations;

/// <summary>
/// Reglas de validación de la entidad Customer. El controller la ejecuta antes de Create y Update.
/// </summary>
public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email format is invalid");

        RuleFor(c => c.Address.Street)
            .NotEmpty().WithMessage("Address street is required");

        RuleFor(c => c.Address.City)
            .NotEmpty().WithMessage("Address city is required");

        RuleFor(c => c.Address.State)
            .NotEmpty().WithMessage("Address state is required");

        RuleFor(c => c.Address.ZipCode)
            .NotEmpty().WithMessage("Address zip code is required");
    }
}
