using DevsuApi.Features.Transfers.CreateTransfer;
using DevsuApi.Features.Transfers.PatchUpdateTransfer;
using DevsuApi.Features.Transfers.UpdateTransfer;
using FluentValidation;

namespace DevsuApi.Features.Transfers.Shared;

public static class TransferValidators
{
    public sealed class BaseTransferPropertiesValidator : AbstractValidator<BaseTransferCommand>
    {
        public BaseTransferPropertiesValidator()
        {
            RuleFor(c => c.Amount).GreaterThanOrEqualTo(1)
                .WithMessage("El monto del movimiento debe de ser mayor o igual a 1.");
        }
    }

    public sealed class CreateTransferCommandValidator : AbstractValidator<CreateTransferCommand>
    {
        public CreateTransferCommandValidator()
        {
            // Include shared rules from base class
            Include(new BaseTransferPropertiesValidator());

            RuleFor(c => c.AccountId)
                .NotEqual(default(Guid))
                .WithMessage("El ID de la cuenta debe de ser valido y la cuenta debe de existir.");
        }
    }

    public sealed class UpdateTransferCommandValidator : AbstractValidator<UpdateTransferCommand>
    {
        public UpdateTransferCommandValidator()
        {
            // Include shared rules from base class
            Include(new BaseTransferPropertiesValidator());
        }
    }

    public sealed class PatchUpdateTransferCommandValidator : AbstractValidator<PatchUpdateTransferCommand>
    {
        public PatchUpdateTransferCommandValidator()
        {
            RuleFor(c => c.Amount)
                .GreaterThanOrEqualTo(1)
                .When(c => null != c.Amount)
                .WithMessage("El monto del movimiento debe de ser mayor o igual a 1.");
        }
    }
}
