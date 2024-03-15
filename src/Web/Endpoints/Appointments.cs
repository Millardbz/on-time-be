using Microsoft.AspNetCore.Mvc;
using on_time_be.Application.Commands.Appointment;
using on_time_be.Application.Common.Models;
using on_time_be.Application.Queries.Appointment;
using on_time_be.Domain.DTOs;

namespace on_time_be.Web.Endpoints;

public class Appointments : EndpointGroupBase
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
            .MapGet(GetAppointmentList)
            .MapPost(CreateAppointment)
            .MapGet("{id}", GetAppointment); 
    }

    public Task<PaginatedList<AppointmentDto>> GetAppointmentList(ISender sender, [AsParameters] GetAppointmentListQuery query)
    {
        return sender.Send(query);
    }

    public Task<Guid> CreateAppointment(ISender sender, CreateAppointmentCommand command)
    {
        return sender.Send(command);
    }
    
    public Task<AppointmentDto> GetAppointment(ISender sender, [FromRoute] Guid id) // Use [FromRoute] to bind the "id" route parameter to the query
    {
        return sender.Send(new GetAppointmentQuery(id));
    }
}
