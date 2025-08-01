using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using DevsuApi.Domain.Shared;
using DevsuApi.Features.Clients.Shared;
using FluentValidation;
using Mapster;
using MediatR;

namespace DevsuApi.Features.Clients.UpdateClient;

public sealed class UpdateClientHandler(
    IClientRepository clientRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateClientCommand> validator) : IRequestHandler<UpdateClientCommand, Result<ClientResponse>>
{
    public async Task<Result<ClientResponse>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        Client? existantClient = await clientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existantClient is null)
        {
            return Result.Failure<ClientResponse>(new Error(
                "UpdateClient.Null",
                "The client with the specified ID was not found"));
        }

        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure<ClientResponse>(new Error(
                "UpdateClient.Validation",
                validationResult.ToString()));
        }

        existantClient.Update(
            request.Name,
            request.Gender,
            request.DateOfBirth,
            request.Identification,
            request.Address,
            request.PhoneNumber,
            request.Password,
            request.Status);

        clientRepository.Update(existantClient);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return existantClient.Adapt<ClientResponse>();
    }
}
