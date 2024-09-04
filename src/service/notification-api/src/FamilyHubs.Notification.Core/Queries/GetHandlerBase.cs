using AutoMapper;
using AutoMapper.QueryableExtensions;
using FamilyHubs.Notification.Data.Entities;
using FamilyHubs.Notification.Data.Repository;
using FamilyHubs.Notification.Api.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Notification.Core.Queries;

public class GetHandlerBase
{
    private readonly IMapper _mapper;
    
    protected GetHandlerBase(ApplicationDbContext context, IMapper mapper)
    {
        Context = context;
        _mapper = mapper;
    }
    
    protected ApplicationDbContext Context { get; }
    
    protected async Task<PaginatedList<MessageDto>> GetPaginatedList(bool requestIsNull, IQueryable<SentNotification> referralList, int pageNumber, int pageSize)
    {
        int totalRecords = referralList.Count();
        List<MessageDto> pagelist;
        if (!requestIsNull)
        {
            pagelist = await referralList.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return new PaginatedList<MessageDto>(pagelist, totalRecords, pageNumber, pageSize);
        }

        pagelist = _mapper.Map<List<MessageDto>>(referralList);
        var result = new PaginatedList<MessageDto>(pagelist.ToList(), totalRecords, 1, 10);
        return result;
    }

    protected IQueryable<SentNotification> OrderBy(IQueryable<SentNotification> currentList, NotificationOrderBy? orderBy, bool? isAscending)
    {
        if (orderBy == null || isAscending == null)
            return currentList;

        switch (orderBy)
        {
            case NotificationOrderBy.RecipientEmail:
                if (isAscending.Value)
                    return currentList.OrderBy(sn => sn.Notified.Select(n => n.Value).FirstOrDefault());
                return currentList.OrderByDescending(sn => sn.Notified.Select(n => n.Value).FirstOrDefault());
                
            case NotificationOrderBy.Created:
                if (isAscending.Value)
                    return currentList.OrderBy(x => x.Created);
                return currentList.OrderByDescending(x => x.Created);

            case NotificationOrderBy.TemplateId:
                if (isAscending.Value)
                    return currentList.OrderBy(x => x.TemplateId);
                return currentList.OrderByDescending(x => x.TemplateId);

            case NotificationOrderBy.ApiKeyType:
                if (isAscending.Value)
                    return currentList.OrderBy(x => x.ApiKeyType);
                return currentList.OrderByDescending(x => x.ApiKeyType);


        }

        return currentList;
    }
}
