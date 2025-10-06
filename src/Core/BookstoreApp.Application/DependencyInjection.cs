using BookstoreApp.Application.Features.Books;
using BookstoreApp.Application.Features.Books.AddBook;
using BookstoreApp.Application.Features.Books.BookedBook;
using BookstoreApp.Application.Features.Books.FreeBooks;
using BookstoreApp.Application.Features.Books.GetBook;
using BookstoreApp.Application.Features.Books.GetBooks;
using BookstoreApp.Application.Features.Books.RemoveBook;
using BookstoreApp.Application.Features.Books.RentBook;
using BookstoreApp.Application.Pipelines;
using BookstoreApp.Domain.Books;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IQueryHandler<GetBookQuery, BookDto>, GetBookQueryHandler>();
        services.AddScoped<
            IQueryHandler<GetBooksQuery, IEnumerable<BookDto>>,
            GetBooksQueryHandler
        >();
        services.AddScoped<ICommandHandler<AddBookCommand>, AddBookCommandHandler>();
        services.AddScoped<ICommandHandler<FreeBookCommand>, FreeBookCommandHandler>();
        services.AddScoped<ICommandHandler<RemoveBookCommand>, RemoveBookCommandHandler>();
        services.AddScoped<ICommandHandler<BookedBookCommand>, BookedBookCommandHandler>();
        services.AddScoped<ICommandHandler<RentBookCommand>, RentBookCommandHandler>();

        

        return services;
    }
}
