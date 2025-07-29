using DevsuApi.Domain.Enums;

namespace DevsuApi.Features.Clients.CreateClient;

public record CreateClientRequest
(
    string Name,
    Gender Gender,
    DateTime DateOfBirth,
    string Identification,
    string Address,
    string PhoneNumber,
    string Password
);
