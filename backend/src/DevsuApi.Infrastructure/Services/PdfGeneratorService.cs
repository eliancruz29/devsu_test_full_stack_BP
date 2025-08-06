using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Services;
using DevsuApi.Infrastructure.Extensions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace DevsuApi.Infrastructure.Services;

public class PdfGeneratorService : IPdfGeneratorService
{
    public byte[] GeneratePdf(IEnumerable<TransfersReport> transfersReport)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        QuestPDF.Settings.EnableDebugging = true;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(3);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("Cliente").Bold();
                        header.Cell().Text("Fecha");
                        header.Cell().Text("Numero Cuenta");
                        header.Cell().Text("Tipo");
                        header.Cell().Text("Saldo Inicial");
                        header.Cell().Text("Movimiento");
                        header.Cell().Text("Saldo Disponible");
                        header.Cell().Text("Estado");
                    });

                    foreach (var item in transfersReport)
                    {
                        table.Cell().Text(item.ClientName);
                        table.Cell().Text(item.Date.ToString("MMM dd, yyyy HH:mm:ss a"));
                        table.Cell().Text(item.AccountNumber);
                        table.Cell().Text(item.Type.GetDatabaseName());
                        table.Cell().Text($"{item.OpeningBalance:N2}");
                        table.Cell().Text($"{item.Amount:N2}");
                        table.Cell().Text($"{item.Balance:N2}");
                        table.Cell().Text(item.Status.GetDatabaseName());
                    }
                });
            });
        });

        return document.GeneratePdf();
    }
}
