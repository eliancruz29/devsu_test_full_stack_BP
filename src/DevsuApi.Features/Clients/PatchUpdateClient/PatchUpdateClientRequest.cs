using DevsuApi.Domain.Enums;

namespace DevsuApi.Features.Clients.PatchUpdateClient;

public record PatchUpdateClientRequest
(
    Guid Id,
    string? Name,
    Gender? Gender,
    DateTime? DateOfBirth,
    string? Identification,
    string? Address,
    string? PhoneNumber,
    string? Password,
    Status? Status
);
