using DevsuApi.Features.Reports.GetTransfersReport;
using DevsuApi.Features.Reports.GetTransfersReportPdf;
using FluentValidation;

namespace DevsuApi.Features.Reports.Shared;

public static class GetTransfersReportValidators
{
    public sealed class BaseAllPropertiesValidator : AbstractValidator<GetTransfersReportQueryBase>
    {
        public BaseAllPropertiesValidator()
        {
            RuleFor(c => c.ClientId).NotEmpty().NotEqual(default(Guid))
                .WithMessage("El ID del cliente debe de ser valido y el cliente debe de existir.");

            RuleFor(c => c.StartDate)
                .NotEmpty()
                .WithMessage("La fecha de inicio es requerida.")
                .NotEqual(default(DateTime))
                .WithMessage("La fecha de inicio debe de ser una fecha valida.")
                .LessThanOrEqualTo(tr => tr.EndDate)
                .WithMessage("La fecha de inicio debe de ser menor o igual a la fecha final.");

            RuleFor(c => c.EndDate)
                .NotEmpty()
                .WithMessage("La fecha final es requerida.")
                .NotEqual(default(DateTime))
                .WithMessage("La fecha final debe de ser una fecha valida.")
                .GreaterThanOrEqualTo(tr => tr.StartDate)
                .WithMessage("La fecha final debe de ser mayor o igual a la fecha de inicio.");
        }
    }

    public sealed class GetTransfersReportQueryValidator : AbstractValidator<GetTransfersReportQuery>
    {
        public GetTransfersReportQueryValidator()
        {
            // Include shared rules from base class
            Include(new BaseAllPropertiesValidator());
        }
    }

    public sealed class GetTransfersReportQueryPdfValidator : AbstractValidator<GetTransfersReportQueryPdf>
    {
        public GetTransfersReportQueryPdfValidator()
        {
            // Include shared rules from base class
            Include(new BaseAllPropertiesValidator());
        }
    }
}
