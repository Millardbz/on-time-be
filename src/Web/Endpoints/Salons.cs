using Microsoft.AspNetCore.Mvc;
using on_time_be.Application.Common.Models;
using on_time_be.Application.Commands.Salon;
using on_time_be.Application.Queries.Salon;
using on_time_be.Domain.DTOs;

namespace on_time_be.Web.Endpoints;

public class Salons : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this);

        if (!app.Environment.IsDevelopment())
        {
            group.RequireAuthorization();
        }

        app.MapGroup(this)
            // .RequireAuthorization()
            .MapGet(GetSalonList)
            .MapPost(CreateSalon)
            .MapGet("{id}", GetSalon); // Map GetSalon to a GET request with an "id" route parameter
    }

    public Task<PaginatedList<SalonDto>> GetSalonList(ISender sender, [AsParameters] GetSalonListQuery query)
    {
        return sender.Send(query);
    }

    public Task<Guid> CreateSalon(ISender sender, CreateSalonCommand command)
    {
        return sender.Send(command);
    }
    
    public Task<SalonDto> GetSalon(ISender sender, [FromRoute] Guid id) // Use [FromRoute] to bind the "id" route parameter to the query
    {
        return sender.Send(new GetSalonQuery(id));
    }
}
