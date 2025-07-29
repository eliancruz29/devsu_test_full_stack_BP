using DevsuTestApi.Enums;
using DevsuTestApi.Features.Clients.Shared;
using DevsuTestApi.Shared;
using MediatR;

namespace DevsuTestApi.Features.Clients.PatchUpdateClient;

public class PatchUpdateClientCommand : IRequest<Result<ClientResponse>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Gender? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Identification { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public Status? Status { get; set; }
}
