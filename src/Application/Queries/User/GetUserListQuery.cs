using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.DTOs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using on_time_be.Application.Common.Mappings;
using on_time_be.Application.Common.Models;

namespace on_time_be.Application.Queries.User;

public record GetUserListQuery() : IRequest<PaginatedList<UserDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    
    public string? Name { get; init; } = null ; 
}

public class GetUserListQueryValidator : AbstractValidator<GetUserListQuery>
{
    public GetUserListQueryValidator()
    {
        RuleFor(query => query.PageNumber).GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least greater than or equal to 1.");
        RuleFor(query => query.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).WithMessage("PageSize must be at least greater than or equal to 1 and less than or equal to 100.");
    }
}

public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, PaginatedList<UserDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UserDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.UserList.AsQueryable();

        if (request.Name is not null)
        {
            query = query.Where(x => x.Name.Contains(request.Name));
        }

        return await query.OrderBy(x => x.Name)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
