using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = on_time_be.Application.Common.Exceptions.ValidationException;

namespace on_time_be.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;

    // Inject the logger
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .ToList();

        if (!failures.Any())
        {
            return await next();
        }

        {
            // Log each validation failure
            foreach (var failure in failures)
            {
                _logger.LogError("Validation failure: {PropertyName} - {ErrorMessage}", failure.PropertyName, failure.ErrorMessage);
            }

            throw new ValidationException(failures);
        }

        // Continue the request handling if there are no validation failures
    }
}
