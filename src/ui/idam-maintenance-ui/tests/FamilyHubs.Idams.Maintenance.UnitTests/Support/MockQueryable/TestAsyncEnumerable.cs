using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
// ReSharper disable StaticMemberInGenericType
// ReSharper disable UnusedMember.Global

namespace FamilyHubs.Idams.Maintenance.UnitTests.Support.MockQueryable;

public class TestAsyncEnumerable<T>: TestQueryProvider<T>, IAsyncEnumerable<T>, IAsyncQueryProvider
{
    private static readonly Type TaskType = typeof(Task);
    
    public TestAsyncEnumerable(Expression expression) : base(expression)
    {
    }

    public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable)
    {
    }

    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
    {
        var expectedResultType = typeof(TResult).GetGenericArguments()[0];
        var executionResult = typeof(IQueryProvider)
            .GetMethods()
            .First(method => method is { Name: nameof(IQueryProvider.Execute), IsGenericMethod: true })
            .MakeGenericMethod(expectedResultType)
            .Invoke(this, [expression]);

        var method = TaskType.GetMethod(nameof(Task.FromResult))!;
        var genericMethod = method.MakeGenericMethod(expectedResultType);
        return (TResult)genericMethod.Invoke(null, [executionResult])!;
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }
}