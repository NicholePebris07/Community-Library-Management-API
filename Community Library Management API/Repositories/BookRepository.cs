using Community_Library_Management_API.Data;
using Community_Library_Management_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Community_Library_Management_API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> AddAsync(Book book)
        {
            _context.Books.Add(book);

            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            var existingBook = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == book.Id);

            if (existingBook == null)
            {
                return false;
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.ISBN = book.ISBN;
            existingBook.Category = book.Category;
            existingBook.TotalCopies = book.TotalCopies;
            existingBook.AvailableCopies = book.AvailableCopies;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}