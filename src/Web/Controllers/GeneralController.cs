using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using on_time_be.WebUI.Controllers;
using Newtonsoft.Json;

public interface IAppRequest<TResponse> { }

public interface ICommand : IAppRequest<Unit> { }

public interface IQuery<TResponse> : IAppRequest<TResponse> { }

[Route("api/[controller]")]
    public class GeneralController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> HandleRequest([FromBody] object request, CancellationToken cancellationToken)
        {
            var requestType = request.GetType();
            var interfaceType = typeof(IAppRequest<>);
            // Check if the request implements IAppRequest<TResponse>
            if (!requestType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType))
            {
                return BadRequest("Invalid request type.");
            }

            // Using dynamic to bypass compile-time type checking. The actual handling is deferred to MediatR.
            dynamic response = await Mediator.Send((dynamic)request, cancellationToken);

            return Ok(response);
        }
    }

