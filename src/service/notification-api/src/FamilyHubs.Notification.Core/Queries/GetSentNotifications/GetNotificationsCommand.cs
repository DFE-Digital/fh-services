using Ardalis.GuardClauses;
using AutoMapper;
using FamilyHubs.Notification.Data.Entities;
using FamilyHubs.Notification.Data.Repository;
using FamilyHubs.Notification.Api.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace FamilyHubs.Notification.Core.Queries.GetSentNotifications;

public class GetNotificationsCommand : IRequest<PaginatedList<MessageDto>>
{
    public GetNotificationsCommand(ApiKeyType? apiKeyType, NotificationOrderBy? notificationOrderBy, bool? isAscending, int? pageNumber, int? pageSize)
    {
        ApiKeyType = apiKeyType;
        OrderBy = notificationOrderBy;
        IsAscending = isAscending;
        PageNumber = pageNumber ?? 1;
        PageSize = pageSize ?? 10;
    }

    public ApiKeyType? ApiKeyType { get; }
    public NotificationOrderBy? OrderBy { get; }
    public bool? IsAscending { get; }
    public int PageNumber { get; } 
    public int PageSize { get; }
}

public class GetNotificationsCommandHandler : GetHandlerBase, IRequestHandler<GetNotificationsCommand, PaginatedList<MessageDto>>
{
    public GetNotificationsCommandHandler(ApplicationDbContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    public async Task<PaginatedList<MessageDto>> Handle(GetNotificationsCommand request, CancellationToken cancellationToken)
    {
        var entities = Context.SentNotifications
            .Include(x => x.TokenValues)
            .Include(x => x.Notified)
            .AsNoTracking();

        if (entities == null)
        {
            throw new NotFoundException(nameof(SentNotification), "");
        }

        if (request.ApiKeyType != null)
        {
            entities = Context.SentNotifications.Where(x => x.ApiKeyType == request.ApiKeyType);
        }

        entities = OrderBy(entities, request.OrderBy, request.IsAscending);

        return await GetPaginatedList(request is null, entities, request?.PageNumber ?? 1, request?.PageSize ?? 10);
    }
}
