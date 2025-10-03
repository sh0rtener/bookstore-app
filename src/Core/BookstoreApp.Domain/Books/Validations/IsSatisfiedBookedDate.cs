using System.Linq.Expressions;
using BookstoreApp.Domain.Common.Specifications;

namespace BookstoreApp.Domain.Books.Validations;

public class IsSatisfiedBookedDate : Specification<DateTime>
{
    public override Expression<Func<DateTime, bool>> ToExpression()
    {
        return new IsDatetimeLessThan(DateTime.Now.AddDays(7)).ToExpression();
    }
}
