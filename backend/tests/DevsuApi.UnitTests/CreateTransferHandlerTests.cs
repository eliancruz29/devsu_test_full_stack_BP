using System;
using System.Threading;
using System.Threading.Tasks;
using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Enums;
using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using DevsuApi.Features.Transfers.CreateTransfer;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace DevsuApi.UnitTests.Features.Transfers.CreateTransfer;

public class CreateTransferHandlerTests
{
    private readonly Mock<ITransferRepository> _transferRepositoryMock = new();
    private readonly Mock<IAccountRepository> _accountRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IValidator<CreateTransferCommand>> _validatorMock = new();
    private readonly CreateTransferHandler _handler;

    public CreateTransferHandlerTests()
    {
        _handler = new CreateTransferHandler(
            _transferRepositoryMock.Object,
            _accountRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsTransferId()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var command = new CreateTransferCommand
        {
            AccountId = accountId,
            Type = TransferTypes.Credit,
            Amount = 500
        };

        var account = Account.Create(
            Guid.NewGuid(),
            "123456789",
            AccountTypes.Savings,
            1000);

        _validatorMock.Setup(x => x.Validate(command))
            .Returns(new ValidationResult());

        _accountRepositoryMock.Setup(x => x.GetByIdWithTransfersAsync(accountId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(account);

        _transferRepositoryMock.Setup(x => x.Add(It.IsAny<Transfer>()));
        _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(1));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(default, result.Value);
        _transferRepositoryMock.Verify(x => x.Add(It.IsAny<Transfer>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidRequest_ReturnsValidationError()
    {
        // Arrange
        var command = new CreateTransferCommand();
        var validationFailure = new ValidationFailure("Amount", "Amount must be positive");
        var validationResult = new ValidationResult([validationFailure]);

        _validatorMock.Setup(x => x.Validate(command))
            .Returns(validationResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("CreateTransfer.Validation", result.Error.Code);
    }

    [Fact]
    public async Task Handle_AccountNotFound_ReturnsError()
    {
        // Arrange
        var command = new CreateTransferCommand { AccountId = Guid.NewGuid() };

        _validatorMock.Setup(x => x.Validate(command))
            .Returns(new ValidationResult());

        _accountRepositoryMock.Setup(x => x.GetByIdWithTransfersAsync(command.AccountId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Account?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal("CreateTransfer.AccountNotFound", result.Error.Code);
    }
}