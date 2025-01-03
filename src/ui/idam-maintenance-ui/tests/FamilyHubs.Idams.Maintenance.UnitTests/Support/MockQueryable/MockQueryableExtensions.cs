namespace FamilyHubs.Idams.Maintenance.UnitTests.Support.MockQueryable;

public static class MockQueryableExtensions
{
	public static IQueryable<TEntity> BuildMock<TEntity>(this IEnumerable<TEntity> data) where TEntity : class
	{
		return new TestAsyncEnumerable<TEntity>(data);
	}
}