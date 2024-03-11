using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using on_time_be.Application.Common.Interfaces;
using on_time_be.Application.Common.Mappings;
using on_time_be.Application.Common.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace on_time_be.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public record GetTodoItemsWithPaginationQuery() : IRequest<PaginatedList<TodoItemBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    
    public string? Title { get; init; } = null ; 
}

public class GetTodoItemsWithPaginationQueryValidator : AbstractValidator<GetTodoItemsWithPaginationQuery>
{
    public GetTodoItemsWithPaginationQueryValidator()
    {
        RuleFor(x => x.ListId).NotEmpty().WithMessage("ListId is required");
        RuleFor(query => query.PageNumber).GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least greater than or equal to 1.");
        RuleFor(query => query.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).WithMessage("PageSize must be at least greater than or equal to 1 and less than or equal to 100.");
    }
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<TodoItemBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TodoItemBriefDto>> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TodoItems
            .Where(x => x.ListId == request.ListId)
            .AsQueryable();

        if (request.Title is not null)
        {
            query = query.Where(x => x.Title != null && x.Title.Contains(request.Title));
        }

        return await query.OrderBy(x => x.Title)
            .ProjectTo<TodoItemBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
