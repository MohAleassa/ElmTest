using elm_test.Models;
using elm_test.Repositories;
using elm_test.Services;
using Microsoft.AspNetCore.Mvc;

namespace elm_test.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLogging();

            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBookService, BookService>();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapGet("/Books", async (IBookService bookService,
                [FromQuery] string input,
                [FromQuery] int currentPage,
                [FromQuery] int itemsPerPage) =>
            {
                var result = new List<Book>();
                try
                {
                    result = await bookService.GetBooks(input, currentPage, itemsPerPage);
                }
                catch (Exception ex)
                {
                    return Results.Problem("Error Occured");
                }
                return Results.Ok(result);
            })
            .WithName("GetBooks");

            app.Run();
        }
    }
}