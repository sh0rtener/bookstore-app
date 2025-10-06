using System.Linq.Expressions;
using BookstoreApp.Domain.Common.Specifications;

namespace BookstoreApp.Domain.Books.Validations;

/// <summary>
/// Спецификация с самой простой проверкой на длину и нулл
/// </summary>
public class IsSatisfiedBookTitle : Specification<string>
{
    public override Expression<Func<string, bool>> ToExpression()
    {
        return x => !string.IsNullOrEmpty(x) && x.Length <= 50;
    }
}
