using System;
using System.Threading;
using System.Threading.Tasks;
using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Enums;
using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using DevsuApi.Features.Accounts.CreateAccount;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

public class CreateAccountHandlerTests
{
    private readonly Mock<IAccountRepository> _accountRepoMock = new();
    private readonly Mock<IClientRepository> _clientRepoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IValidator<CreateAccountCommand>> _validatorMock = new();

    private readonly CreateAccountHandler _handler;

    public CreateAccountHandlerTests()
    {
        _handler = new CreateAccountHandler(
            _accountRepoMock.Object,
            _clientRepoMock.Object,
            _unitOfWorkMock.Object,
            _validatorMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenValidationFails()
    {
        // Arrange
        var command = GetValidCommand();
        _validatorMock.Setup(v => v.Validate(command))
            .Returns(new ValidationResult([new ValidationFailure("Field", "Error")]));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("CreateAccount.Validation", result.Error.Code);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenClientNotFound()
    {
        // Arrange
        var command = GetValidCommand();

        _validatorMock.Setup(v => v.Validate(command))
            .Returns(new ValidationResult());

        _clientRepoMock.Setup(r => r.GetByIdWithAccountsAsync(command.ClientId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Client?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("CreateAccount.ClientNotFound", result.Error.Code);
    }

    [Fact]
    public async Task Handle_ShouldReturnAccountId_WhenSuccessful()
    {
        // Arrange
        var command = GetValidCommand();
        var newAccountId = Guid.NewGuid();
        var client = new Mock<Client>(Guid.NewGuid(), "Jane Doe", "123", "password", true) { CallBase = true };

        client.Setup(c => c.AddAccount(command.AccountNumber, command.Type, command.OpeningBalance))
              .Returns(Account.Create(client.Object.Id, command.AccountNumber, command.Type, command.OpeningBalance));

        _validatorMock.Setup(v => v.Validate(command))
            .Returns(new ValidationResult());

        _clientRepoMock.Setup(r => r.GetByIdWithAccountsAsync(command.ClientId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(client.Object);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(newAccountId, result.Value);

        _accountRepoMock.Verify(r => r.Add(It.IsAny<Account>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    private static CreateAccountCommand GetValidCommand() =>
        new CreateAccountCommand
        {
            ClientId = Guid.NewGuid(),
            AccountNumber = "ACC-0001",
            Type = AccountTypes.Savings,
            OpeningBalance = 1000
        };
}
