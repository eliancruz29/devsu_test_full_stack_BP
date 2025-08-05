using DevsuApi.Features.Clients.CreateClient;
using DevsuApi.Features.Clients.PatchUpdateClient;
using DevsuApi.Features.Clients.UpdateClient;
using FluentValidation;

namespace DevsuApi.Features.Clients.Shared;

public static class ClientValidators
{
    public sealed class BaseAllPropertiesValidator : AbstractValidator<BaseClientCommand>
    {
        public BaseAllPropertiesValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.DateOfBirth).NotEmpty().NotEqual(default(DateTime))
                .WithMessage("La fecha de nacimiento debe de ser una fecha valida.");
            RuleFor(c => c.Identification).NotEmpty().Matches(@"^\d{3}-\d{7}-\d{1}$|^\d{11}$")
                .WithMessage("La identificación debe de ser en un formato valido para Republica Dominicana (###-#######-# u 11 digitos).");
            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.PhoneNumber).NotEmpty().Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("El número de teléfono debe de ser en un formato internacional valido.");
            RuleFor(c => c.Password).NotEmpty().MinimumLength(6)
                .WithMessage("La clave debe de ser al menos de 6 caracteres de longitud.");
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
                .WithMessage("La identificación debe de ser en un formato valido para Republica Dominicana (###-#######-# u 11 digitos).");
            RuleFor(c => c.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .When(c => !string.IsNullOrWhiteSpace(c.PhoneNumber))
                .WithMessage("El número de teléfono debe de ser en un formato internacional valido.");
            RuleFor(c => c.Password)
                .MinimumLength(6)
                .When(c => !string.IsNullOrWhiteSpace(c.Password))
                .WithMessage("La clave debe de ser al menos de 6 caracteres de longitud.");
        }
    }
}
