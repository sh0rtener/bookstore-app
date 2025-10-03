using System.Linq.Expressions;

namespace BookstoreApp.Domain.Common.Specifications;

public class IsDatetimeMoreThanYesterday : Specification<DateTime>
{
    public override Expression<Func<DateTime, bool>> ToExpression()
    {
        return x => DateTime.Now.AddDays(-1) < x;
    }
}
