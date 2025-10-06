using System.Linq.Expressions;
using BookstoreApp.Domain.Common.Specifications;

namespace BookstoreApp.Domain.Books.Validations;

public class IsSatisfiedBookPagesCount : Specification<int>
{
    public override Expression<Func<int, bool>> ToExpression()
    {
        return x => x >= 0 && x <= ushort.MaxValue;
    }
}
