using FamilyHubs.Report.Data.Entities;

namespace FamilyHubs.Report.Data.Repository;

public interface IReportDbContext
{
    public IQueryable<ServiceSearchFact> ServiceSearchFacts { get; }

    public IQueryable<ConnectionRequestsSentFact> ConnectionRequestsSentFacts { get; }

    public void AddServiceSearchFact(ServiceSearchFact serviceSearchFact);

    public void AddDateDim(DateDim dateDim);

    public void AddTimeDim(TimeDim timeDim);

    public void AddServiceSearchesDim(ServiceSearchesDim serviceSearchesDim);

    public void AddOrganisationDim(OrganisationDim organisationDim);

    public void AddConnectionRequestsSentFact(ConnectionRequestsSentFact connectionRequestsSentFact);

    Task<int> ExecuteRawSql(FormattableString sql, CancellationToken cancellationToken = default);

    Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default);

    Task<List<T>> ToListAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default);

    Task<int> CountAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default);
    public Task<int> SaveChangesAsync();
}
