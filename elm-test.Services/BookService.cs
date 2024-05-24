using elm_test.Models;
using elm_test.Repositories;

namespace elm_test.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> GetBooks(string input, int currentPage, int itemsPerPage)
        {
            var skip = (currentPage - 1) * itemsPerPage;

            return await _bookRepository.GetBooks(input, skip, itemsPerPage);
        }
    }
}
