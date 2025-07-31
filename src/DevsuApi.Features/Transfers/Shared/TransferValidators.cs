using DevsuApi.Features.Transfers.CreateTransfer;
using DevsuApi.Features.Transfers.PatchUpdateTransfer;
using FluentValidation;

namespace DevsuApi.Features.Transfers.Shared;

public static class TransferValidators
{
    public sealed class CreateTransferCommandValidator : AbstractValidator<CreateTransferCommand>
    {
        public CreateTransferCommandValidator()
        {
            RuleFor(c => c.Amount).GreaterThanOrEqualTo(1)
                .WithMessage("The amount of the transfer must be greater than or equal to 1.");
        }
    }

    public sealed class PatchUpdateTransferCommandValidator : AbstractValidator<PatchUpdateTransferCommand>
    {
        public PatchUpdateTransferCommandValidator()
        {
            RuleFor(c => c.Amount)
                .GreaterThanOrEqualTo(1)
                .When(c => null != c.Amount)
                .WithMessage("Amount must be greater than or equal to 1.");
        }
    }
}
