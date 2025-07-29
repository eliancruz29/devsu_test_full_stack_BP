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
                .WithMessage("Opening balance must be greater than or equal to 0.");
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
                .WithMessage("The client ID must be a valid and existent Client.");
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
                .WithMessage("Opening balance must be greater than or equal to 0.");
        }
    }
}
