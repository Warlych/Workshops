using FluentValidation;
using ServiceA.Handlers.Commands;

namespace ServiceA.PipelineBehaviors.FluentValidator.Validators;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Goods).NotNull().WithMessage("Goods cannot be null");
    }
}
