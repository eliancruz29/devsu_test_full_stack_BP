using DevsuApi.Features.Accounts.CreateAccount;
using FluentValidation;

namespace DevsuApi.Features.Accounts.Shared;

public static class AccountValidators
{
    public sealed class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(c => c.ClientId).NotEqual(default(Guid))
                .WithMessage("The client ID must be a valid and existent Client.");
            RuleFor(c => c.AccountNumber).NotEmpty();
            RuleFor(c => c.OpeningBalance).GreaterThanOrEqualTo(0)
                .WithMessage("Opening balance must be greater than or equal to 0.");
        }
    }
}
