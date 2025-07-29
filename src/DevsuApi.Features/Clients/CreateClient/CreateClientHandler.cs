using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using DevsuApi.Domain.Shared;
using FluentValidation;
using MediatR;

namespace DevsuApi.Features.Clients.CreateClient;

public sealed class CreateClientHandler(
    IClientRepository clientRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateClientCommand> validator) : IRequestHandler<CreateClientCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure<Guid>(new Error(
                "CreateClient.Validation",
                validationResult.ToString()));
        }

        var client = Client.Create(
            request.Name,
            request.Gender,
            request.DateOfBirth,
            request.Identification,
            request.Address,
            request.PhoneNumber,
            request.Password);

        clientRepository.Add(client);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return client.Id;
    }
}
