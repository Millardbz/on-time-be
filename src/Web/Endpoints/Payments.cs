using Microsoft.AspNetCore.Mvc;
using on_time_be.Application.Commands.Payment;
using on_time_be.Application.Common.Models;
using on_time_be.Application.Queries.Payment;
using on_time_be.Domain.DTOs;

namespace on_time_be.Web.Endpoints;

public class Payments : EndpointGroupBase
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
            .MapGet(GetPaymentList)
            .MapPost(CreatePayment)
            .MapGet("{id}", GetPayment); 
    }

    public Task<PaginatedList<PaymentDto>> GetPaymentList(ISender sender, [AsParameters] GetPaymentListQuery query)
    {
        return sender.Send(query);
    }

    public Task<Guid> CreatePayment(ISender sender, CreatePaymentCommand command)
    {
        return sender.Send(command);
    }
    
    public Task<PaymentDto> GetPayment(ISender sender, [FromRoute] Guid id) // Use [FromRoute] to bind the "id" route parameter to the query
    {
        return sender.Send(new GetPaymentQuery(id));
    }
}
