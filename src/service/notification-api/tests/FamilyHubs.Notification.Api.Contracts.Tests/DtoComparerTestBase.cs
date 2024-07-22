using FluentAssertions;
using System.Linq.Expressions;
using System.Reflection;

namespace FamilyHubs.Notification.Api.Contracts.Tests.csproj;
public abstract class DtoComparerTestBase<T, TPropertyType> where T : DtoBase<long>
{
    private readonly T _sut;
    private T _other;
    private readonly Expression<Func<T, TPropertyType?>> _propertyToTest;

    protected DtoComparerTestBase(T sut, T other, Expression<Func<T, TPropertyType?>> propertyToTest)
    {
        _sut = sut;
        _other = other;
        _propertyToTest = propertyToTest;
    }

    [Fact]
    public void WhenComparingTwoObjectsWithSameValuesThenReturnsTrue()
    {
        _sut.Equals(_other).Should().BeTrue();
    }

    [Fact]
    public void WhenComparingTwoObjectsIgnorePrimaryAndForeignKeyThenReturnsTrue()
    {
        _sut.Id = 1;
        _other.Id = 2;
        _sut.Equals(_other).Should().BeTrue();
    }

    [Fact]
    public void WhenComparingTwoObjectsWithSameReferenceThenReturnsTrue()
    {
        _other = _sut;
        _sut.Equals(_other).Should().BeTrue();
    }

    [Fact]
    public void WhenObjectIsNullThenReturnsFalse()
    {
        _other = null!;
        _sut.Equals(_other).Should().BeFalse();
    }

    [Fact]
    public void WhenComparingTwoObjectsWithDifferentValuesThenReturnsFalse()
    {
        var propertyInfo = GetPropertyInfo();

        var propertyInfoPropertyType = propertyInfo!.PropertyType;

        if (propertyInfoPropertyType == typeof(int?) || propertyInfoPropertyType == typeof(int)) propertyInfo.SetValue(_other, Random.Shared.Next());
        if (propertyInfoPropertyType == typeof(long?) || propertyInfoPropertyType == typeof(long)) propertyInfo.SetValue(_other, Random.Shared.Next());
        if (propertyInfoPropertyType == typeof(DateTime?) || propertyInfoPropertyType == typeof(DateTime)) propertyInfo.SetValue(_other, DateTime.Now);

        if (propertyInfoPropertyType.BaseType == typeof(Enum)) propertyInfo.SetValue(_other, null);

        if (propertyInfoPropertyType == typeof(string)) propertyInfo.SetValue(_other, Random.Shared.Next().ToString());

        _sut.Equals(_other).Should().BeFalse();
    }

    [Fact]
    public void WhenOtherObjectPropertyIsNullThenReturnsFalse()
    {
        var propertyInfo = GetPropertyInfo();

        propertyInfo.Should().NotBeNull();

        var propertyInfoPropertyType = propertyInfo!.PropertyType;

        if (propertyInfoPropertyType == typeof(int?)) propertyInfo.SetValue(_other, null);
        if (propertyInfoPropertyType == typeof(int)) propertyInfo.SetValue(_other, -1);
        if (propertyInfoPropertyType == typeof(string)) propertyInfo.SetValue(_other, null);
        if (propertyInfoPropertyType == typeof(long)) propertyInfo.SetValue(_other, -1);
        if (propertyInfoPropertyType == typeof(long?)) propertyInfo.SetValue(_other, null);

        if (propertyInfoPropertyType.BaseType == typeof(Enum)) propertyInfo.SetValue(_other, null);

        if (propertyInfoPropertyType == typeof(DateTime?) || propertyInfoPropertyType == typeof(DateTime)) propertyInfo.SetValue(_other, null);


        _sut.Equals(_other).Should().BeFalse();
    }

    private PropertyInfo? GetPropertyInfo()
    {
        PropertyInfo? propertyInfo = null;

        if (_propertyToTest.Body is MemberExpression { Member: PropertyInfo memberPropertyInfo })
        {
            propertyInfo = memberPropertyInfo;
        }

        if (_propertyToTest.Body is UnaryExpression { Operand: MemberExpression { Member: PropertyInfo unaryPropertyInfo } })
        {
            propertyInfo = unaryPropertyInfo;
        }

        return propertyInfo;
    }
}