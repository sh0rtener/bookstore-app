using System.Linq.Expressions;
using BookstoreApp.Domain.Common.Specifications;

namespace BookstoreApp.Domain.Books.Validations;

public class IsSatisfiedBookDescription : Specification<string>
{
    public override Expression<Func<string, bool>> ToExpression()
    {
        return x => true;
    }
}
