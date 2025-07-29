using DevsuTestApi.Features.Clients.CreateClient;
using DevsuTestApi.Features.Clients.PatchUpdateClient;
using DevsuTestApi.Features.Clients.UpdateClient;
using FluentValidation;

namespace DevsuTestApi.Features.Clients.Shared;

public static class ClientValidators
{
    public sealed class BaseAllPropertiesValidator : AbstractValidator<BaseClientCommand>
    {
        public BaseAllPropertiesValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.DateOfBirth).NotEmpty().NotEqual(default(DateTime))
                .WithMessage("Date of Birth must be a valid date.");
            RuleFor(c => c.Identification).NotEmpty().Matches(@"^\d{3}-\d{7}-\d{1}$|^\d{11}$")
                .WithMessage("Identification must be a valid Dominican Republic ID format (###-#######-# or 11 digits).");
            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.PhoneNumber).NotEmpty().Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Phone number must be a valid international format.");
            RuleFor(c => c.Password).NotEmpty().MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.");
        }
    }

    public sealed class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            // Include shared rules from base class
            Include(new BaseAllPropertiesValidator());
        }
    }

    public sealed class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            // Include shared rules from base class
            Include(new BaseAllPropertiesValidator());
        }
    }

    public sealed class PartialPropertiesValidator : AbstractValidator<PatchUpdateClientCommand>
    {
        public PartialPropertiesValidator()
        {
            RuleFor(c => c.Identification)
                .Matches(@"^\d{3}-\d{7}-\d{1}$|^\d{11}$")
                .When(c => !string.IsNullOrWhiteSpace(c.Identification))
                .WithMessage("Identification must be a valid Dominican Republic ID format (###-#######-# or 11 digits).");
            RuleFor(c => c.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .When(c => !string.IsNullOrWhiteSpace(c.PhoneNumber))
                .WithMessage("Phone number must be a valid international format.");
            RuleFor(c => c.Password)
                .MinimumLength(6)
                .When(c => !string.IsNullOrWhiteSpace(c.Password))
                .WithMessage("Password must be at least 6 characters long.");
        }
    }
}
