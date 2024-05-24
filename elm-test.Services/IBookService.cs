using elm_test.Models;

namespace elm_test.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetBooks(string input, int currentPage, int itemsPerPage);
    }
}
