using DevsuApi.Domain.Enums;
using DevsuApi.Features.Clients.Shared;
using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Clients.PatchUpdateClient;

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
