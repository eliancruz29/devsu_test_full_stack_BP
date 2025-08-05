using DevsuApi.Features.Accounts.CreateAccount;
using DevsuApi.Features.Accounts.PatchUpdateAccount;
using DevsuApi.Features.Accounts.UpdateAccount;
using FluentValidation;

namespace DevsuApi.Features.Accounts.Shared;

public static class AccountValidators
{
    public sealed class BaseAccountPropertiesValidator : AbstractValidator<BaseAccountCommand>
    {
        public BaseAccountPropertiesValidator()
        {
            RuleFor(c => c.OpeningBalance).GreaterThanOrEqualTo(0)
                .WithMessage("El saldo inicial debe de ser mayor o igual a 0.");
            RuleFor(c => c.AccountNumber).NotEmpty();
        }
    }

    public sealed class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            // Include shared rules from base class
            Include(new BaseAccountPropertiesValidator());

            RuleFor(c => c.ClientId).NotEqual(default(Guid))
                .WithMessage("El ID del cliente debe de ser valido y el cliente debe de existir.");
        }
    }

    public sealed class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            // Include shared rules from base class
            Include(new BaseAccountPropertiesValidator());
        }
    }

    public sealed class PatchUpdateAccountCommandValidator : AbstractValidator<PatchUpdateAccountCommand>
    {
        public PatchUpdateAccountCommandValidator()
        {
            RuleFor(c => c.OpeningBalance)
                .GreaterThanOrEqualTo(0)
                .When(c => null != c.OpeningBalance)
                .WithMessage("El saldo inicial debe de ser mayor o igual a 0.");
        }
    }
}
