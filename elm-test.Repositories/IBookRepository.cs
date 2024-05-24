using elm_test.Models;

namespace elm_test.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooks(string input, int skip, int take);
    }
}
