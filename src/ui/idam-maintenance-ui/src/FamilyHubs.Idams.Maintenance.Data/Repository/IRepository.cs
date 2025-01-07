using FamilyHubs.Idams.Maintenance.Data.Entities;

namespace FamilyHubs.Idams.Maintenance.Data.Repository;

public interface IRepository
{
    IQueryable<AccountClaim> AccountClaims { get; }
    IQueryable<Account> Accounts { get; }
    IQueryable<UserSession> UserSessions { get; }

    Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
    void Remove<TEntity>(TEntity entity) where TEntity : class;
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}