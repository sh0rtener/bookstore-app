using System.Linq.Expressions;

namespace BookstoreApp.Domain.Common.Specifications;

public class IsDatetimeLessThan : Specification<DateTime>
{
    private readonly DateTime _value;

    public IsDatetimeLessThan(DateTime value) => _value = value;

    public override Expression<Func<DateTime, bool>> ToExpression()
    {
        return x => x < _value;
    }
}
