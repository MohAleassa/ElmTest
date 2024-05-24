using Dapper;
using elm_test.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace elm_test.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;

        public BookRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<List<Book>> GetBooks(string input, int skip, int take)
        {
            var connection = CreateConnection();

            var sql = $@"SELECT *
                    FROM (
                      SELECT *, 
                        JSON_VALUE(BookInfo, '$.BookTitle') AS BookTitle,
                        JSON_VALUE(BookInfo, '$.BookDescription') AS BookDescription,
                    	JSON_VALUE(BookInfo, '$.Author') AS Author,
                    	JSON_VALUE(BookInfo, '$.PublishDate') AS PublishDate,
                    	CoverBase64 = (SELECT CoverBase64 FROM OPENJSON(BookInfo) WITH (CoverBase64 nvarchar(MAX)))
                      FROM Book
                    ) AS temp
                    WHERE temp.BookTitle LIKE '%{input}%' OR 
                          temp.BookDescription LIKE '%{input}%' OR 
                          temp.Author LIKE '%{input}%' OR
                          temp.PublishDate LIKE '%{input}%'

                    ORDER BY BookId
                    OFFSET {skip} ROWS
                    FETCH NEXT {take} ROWS ONLY";

            var result = await connection.QueryAsync<Book>(sql);

            return (List<Book>)result;
        }
    }
}
