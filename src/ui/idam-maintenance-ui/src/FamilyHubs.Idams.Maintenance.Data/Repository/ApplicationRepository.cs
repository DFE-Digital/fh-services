using System.Diagnostics.CodeAnalysis;
using FamilyHubs.Idams.Maintenance.Data.Entities;

namespace FamilyHubs.Idams.Maintenance.Data.Repository;

[ExcludeFromCodeCoverage]
public sealed class ApplicationRepository : IRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IQueryable<AccountClaim> AccountClaims => _context.AccountClaims;
    public IQueryable<Account> Accounts => _context.Accounts;
    public IQueryable<UserSession> UserSessions => _context.UserSessions;

    public async Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class
    {
        await _context.AddAsync(entity, cancellationToken);
    }

    public void Remove<TEntity>(TEntity entity) where TEntity : class
    {
        _context.Remove(entity);
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}