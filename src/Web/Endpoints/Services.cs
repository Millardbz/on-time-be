using Microsoft.AspNetCore.Mvc;
using on_time_be.Application.Commands.Service;
using on_time_be.Application.Common.Models;
using on_time_be.Application.Queries.Service;
using on_time_be.Domain.DTOs;

namespace on_time_be.Web.Endpoints;

public class Services : EndpointGroupBase
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
            .MapGet(GetServiceList)
            .MapPost(CreateService)
            .MapGet("{id}", GetService); 
    }

    public Task<PaginatedList<ServiceDto>> GetServiceList(ISender sender, [AsParameters] GetServiceListQuery query)
    {
        return sender.Send(query);
    }

    public Task<Guid> CreateService(ISender sender, CreateServiceCommand command)
    {
        return sender.Send(command);
    }
    
    public Task<ServiceDto> GetService(ISender sender, [FromRoute] Guid id) // Use [FromRoute] to bind the "id" route parameter to the query
    {
        return sender.Send(new GetServiceQuery(id));
    }
}
