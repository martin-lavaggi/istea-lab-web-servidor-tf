using FluentValidation;
using Orders.Domain.Models.Aggregates;

namespace Orders.Application.Validations;

/// <summary>
/// Reglas de validación del agregado Order. El controller la ejecuta antes de persistir la orden.
/// </summary>
public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(o => o.Items)
            .NotEmpty().WithMessage("Order must have at least one item");
    }
}
