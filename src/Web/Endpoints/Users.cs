using Microsoft.AspNetCore.Mvc;
using on_time_be.Application.Commands.User;
using on_time_be.Application.Common.Models;
using on_time_be.Application.Queries.User;
using on_time_be.Domain.DTOs;
using on_time_be.Infrastructure.Identity;

namespace on_time_be.Web.Endpoints;

public class Users : EndpointGroupBase
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
            .MapGet(GetUserList)
            .MapPost(CreateUser)
            .MapGet("{id}", GetUser);

        app.MapGroup(this)
            .MapIdentityApi<ApplicationUser>();
    }

    public Task<PaginatedList<UserDto>> GetUserList(ISender sender, [AsParameters] GetUserListQuery query)
    {
        return sender.Send(query);
    }

    public Task<Guid> CreateUser(ISender sender, CreateUserCommand command)
    {
        return sender.Send(command);
    }
    
    public Task<UserDto> GetUser(ISender sender, [FromRoute] Guid id) // Use [FromRoute] to bind the "id" route parameter to the query
    {
        return sender.Send(new GetUserQuery(id));
    }
}
