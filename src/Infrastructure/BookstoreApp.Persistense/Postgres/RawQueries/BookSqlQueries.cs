namespace BookstoreApp.Persistense.Postgres.RawQueries;

public static class BookSqlQueries
{
    public static string GetBookById =
        "SELECT " +
        "   id as \"Id\" " +
        "   , title as \"Title\"" +
        "   , description as \"Description\"" +
        "   , author as \"Author\"" +
        "   , pages_count as \"PagesCount\"" +
        "   , user_name as \"UserName\"" +
        "   , booked_to as \"BookedTo\"" +
        "   , status as \"Status\" " +
        "FROM" +
        "   books b " +
        "WHERE" +
        "   b.id = @Id";

    public static string GetBooks =
        """
        SELECT 	
            id as "Id"
            , title as "Title"
            , description as "Description"
            , author as "Author"
            , pages_count as "PagesCount"
            , user_name as "UserName"
            , booked_to as "BookedTo"
            , status as "Status"
        FROM
            books b 
        WHERE
            b.author ilike @Author
            OR b.title ilike @Title
            OR b.status ilike @Status
        limit @Take
        offset @Skip
        """;

    public static string AddNewBook =
        """
        INSERT INTO 
            books
        (
            id
            , title
            , description
            , author
            , pages_count
            , status
        )
        VALUES
        (
            @Id
            , @Title
            , @Description
            , @Author
            , @PagesCount
            , 'Free'
        )
        """;

    public static string UpdateStatus =
        """
            UPDATE
                books
            SET
                status = @Status
                , user_name = @UserName
                , booked_to = @BookedTo
            WHERE 
                id = @Id
        """;

    public static string Remove =
        """
        DELETE FROM
            books
        WHERE
            id = @Id
        """;
}