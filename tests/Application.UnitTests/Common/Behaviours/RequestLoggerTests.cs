using on_time_be.Application.Common.Behaviours;
using on_time_be.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using on_time_be.Application.Commands.Customer;
using on_time_be.Domain.Enums;

namespace on_time_be.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateCustomerCommand>> _logger = null!;
    private Mock<IUser> _user = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateCustomerCommand>>();
        _user = new Mock<IUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateCustomerCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateCustomerCommand("Test description", "Copenhagen", "Klip 37", "12345678", "10-18"), new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateCustomerCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateCustomerCommand("Test description", "Copenhagen", "Klip 37", "12345678", "10-18"), new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
