using System.Collections;
using System.Linq.Expressions;
// ReSharper disable PossibleMultipleEnumeration

namespace FamilyHubs.Idams.Maintenance.UnitTests.Support.MockQueryable;

public abstract class TestQueryProvider<T> : IOrderedQueryable<T>, IQueryProvider
{
    private IEnumerable<T>? _enumerable;

    protected TestQueryProvider(Expression expression)
    {
        Expression = expression;
    }

    protected TestQueryProvider(IEnumerable<T> enumerable)
    {
        _enumerable = enumerable;
        Expression = enumerable.AsQueryable().Expression;
    }

    public Type ElementType => typeof(T);

    public Expression Expression { get; }

    public IQueryProvider Provider => this;
    
    public IQueryable CreateQuery(Expression expression)
    {
        if (expression is MethodCallExpression method)
        {
            var resultType = method.Method.ReturnType;
            var genericArgument = resultType.GetGenericArguments()[0];
            return (IQueryable)CreateInstance(genericArgument, expression);
        }

        return CreateQuery<T>(expression);
    }

    public IQueryable<TEntity> CreateQuery<TEntity>(Expression expression)
    {
        return (IQueryable<TEntity>)CreateInstance(typeof(TEntity), expression);
    }

    private object CreateInstance(Type tElement, Expression expression)
    {
        var queryType = GetType().GetGenericTypeDefinition().MakeGenericType(tElement);
        return Activator.CreateInstance(queryType, expression)!;
    }

    public object Execute(Expression expression)
    {
        return CompileExpressionItem<object>(expression);
    }

    public TResult Execute<TResult>(Expression expression)
    {
        return CompileExpressionItem<TResult>(expression);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        _enumerable ??= CompileExpressionItem<IEnumerable<T>>(Expression);
        return _enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        _enumerable ??= CompileExpressionItem<IEnumerable<T>>(Expression);
        return _enumerable.GetEnumerator();
    }

    private static TResult CompileExpressionItem<TResult>(Expression expression)
    {
        var visitor = new TestExpressionVisitor();
        var body = visitor.Visit(expression);
        var action = Expression.Lambda<Func<TResult>>(body, (IEnumerable<ParameterExpression>)null!);
        return action.Compile()();
    }
}