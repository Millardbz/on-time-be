using Microsoft.AspNetCore.Mvc;
using on_time_be.Application.Commands.Customer;
using on_time_be.Application.Common.Models;
using on_time_be.Application.Queries.Customer;
using on_time_be.Domain.DTOs;

namespace on_time_be.Web.Endpoints;

public class Customers : EndpointGroupBase
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
            .MapGet(GetCustomerList)
            .MapPost(CreateCustomer)
            .MapGet("{id}", GetCustomer); 
    }

    public Task<PaginatedList<CustomerDto>> GetCustomerList(ISender sender, [AsParameters] GetCustomerListQuery query)
    {
        return sender.Send(query);
    }

    public Task<Guid> CreateCustomer(ISender sender, CreateCustomerCommand command)
    {
        return sender.Send(command);
    }
    
    public Task<CustomerDto> GetCustomer(ISender sender, [FromRoute] Guid id) // Use [FromRoute] to bind the "id" route parameter to the query
    {
        return sender.Send(new GetCustomerQuery(id));
    }
}
