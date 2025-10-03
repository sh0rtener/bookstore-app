using System.Linq.Expressions;
using System.Text.RegularExpressions;
using BookstoreApp.Domain.Common.Specifications;

namespace BookstoreApp.Domain.Books.Validations;

public class IsSatisfiedBookAuthor : Specification<string>
{
    public override Expression<Func<string, bool>> ToExpression()
    {
        return x => !string.IsNullOrEmpty(x) && Regex.IsMatch(x, "[a-zA-Z]");
    }
}