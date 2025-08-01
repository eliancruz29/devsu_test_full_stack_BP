using DevsuApi.Domain.Enums;

namespace DevsuApi.Features.Clients.UpdateClient;

public record UpdateClientRequest
(
    Guid Id,
    string Name,
    Gender Gender,
    DateTime DateOfBirth,
    string Identification,
    string Address,
    string PhoneNumber,
    string Password,
    Status Status
);
