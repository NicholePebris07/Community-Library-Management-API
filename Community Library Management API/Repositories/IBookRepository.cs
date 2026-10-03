using Community_Library_Management_API.Models;

namespace Community_Library_Management_API.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();

        Task<Book?> GetByIdAsync(int id);

        Task<Book> AddAsync(Book book);

        Task<bool> UpdateAsync(Book book);

        Task<bool> DeleteAsync(int id);

        Task<bool> UpdateAvailableCopiesAsync(int bookId, int change);
    }
}