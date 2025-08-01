using MediatR;

namespace DevsuApi.SharedKernel;

public record DomainEvent(Guid Id) : INotification;
