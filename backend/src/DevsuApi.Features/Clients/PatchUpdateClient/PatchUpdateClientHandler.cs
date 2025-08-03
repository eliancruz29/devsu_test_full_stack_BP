using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using DevsuApi.Domain.Shared;
using DevsuApi.Features.Clients.Shared;
using FluentValidation;
using Mapster;
using MediatR;

namespace DevsuApi.Features.Clients.PatchUpdateClient;

public sealed class PatchUpdateClientHandler(
    IClientRepository clientRepository,
    IUnitOfWork unitOfWork,
    IValidator<PatchUpdateClientCommand> validator) : IRequestHandler<PatchUpdateClientCommand, Result<ClientResponse>>
{
    public async Task<Result<ClientResponse>> Handle(PatchUpdateClientCommand request, CancellationToken cancellationToken)
    {
        Client? existantClient = await clientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existantClient is null)
        {
            return Result.Failure<ClientResponse>(new Error(
                "PatchUpdateClient.Null",
                "The client with the specified ID was not found"));
        }

        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure<ClientResponse>(new Error(
                "PatchUpdateClient.Validation",
                validationResult.ToString()));
        }

        existantClient.Update(
            string.IsNullOrWhiteSpace(request.Name) ? existantClient.Name : request.Name,
            request.Gender ?? existantClient.Gender,
            request.DateOfBirth ?? existantClient.DateOfBirth,
            string.IsNullOrWhiteSpace(request.Identification) ? existantClient.Identification : request.Identification,
            string.IsNullOrWhiteSpace(request.Address) ? existantClient.Address : request.Address,
            string.IsNullOrWhiteSpace(request.PhoneNumber) ? existantClient.PhoneNumber : request.PhoneNumber,
            string.IsNullOrWhiteSpace(request.Password) ? existantClient.Password : request.Password,
            request.Status ?? existantClient.Status
        );

        clientRepository.Update(existantClient);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return existantClient.Adapt<ClientResponse>();
    }
}
