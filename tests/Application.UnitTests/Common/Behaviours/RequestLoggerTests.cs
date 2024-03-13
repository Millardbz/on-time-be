using on_time_be.Application.Common.Behaviours;
using on_time_be.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using on_time_be.Application.Commands.Salon;
using on_time_be.Domain.Enums;

namespace on_time_be.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateSalonCommand>> _logger = null!;
    private Mock<IUser> _user = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateSalonCommand>>();
        _user = new Mock<IUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateSalonCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateSalonCommand { Id = new Guid(), Description = "Test description", Location = "Copenhagen", Name = "Klip 37", ContactInformation = "12345678", DepositType = DepositType.Fixed, DepositValue = 50}, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateSalonCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateSalonCommand { Id = new Guid(), Description = "Test description", Location = "Copenhagen", Name = "Klip 37", ContactInformation = "12345678", DepositType = DepositType.Fixed, DepositValue = 50}, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
