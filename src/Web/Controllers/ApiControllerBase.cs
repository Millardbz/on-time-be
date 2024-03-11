using Microsoft.AspNetCore.Mvc;

namespace on_time_be.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator; // Mark the field as nullable.

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    // Add any common functionality that would be shared across all your controllers here.
}
