using Community_Library_Management_API.Dtos.Books;
using Community_Library_Management_API.Models;
using Community_Library_Management_API.Repositories;

namespace Community_Library_Management_API.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BookReadDto>> GetAllAsync()
        {
            var books = await _repository.GetAllAsync();

            return books.Select(book => new BookReadDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Category = book.Category,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies
            }).ToList();
        }

        public async Task<BookReadDto?> GetByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
            {
                return null;
            }

            return new BookReadDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Category = book.Category,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies
            };
        }

        public async Task<BookReadDto> CreateAsync(BookCreateDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                ISBN = dto.ISBN,
                Category = dto.Category,
                TotalCopies = dto.TotalCopies,
                AvailableCopies = dto.AvailableCopies
            };

            var createdBook = await _repository.AddAsync(book);

            return new BookReadDto
            {
                Id = createdBook.Id,
                Title = createdBook.Title,
                Author = createdBook.Author,
                ISBN = createdBook.ISBN,
                Category = createdBook.Category,
                TotalCopies = createdBook.TotalCopies,
                AvailableCopies = createdBook.AvailableCopies
            };
        }

        public async Task<bool> UpdateAsync(int id, BookUpdateDto dto)
        {
            var book = new Book
            {
                Id = id,
                Title = dto.Title,
                Author = dto.Author,
                ISBN = dto.ISBN,
                Category = dto.Category,
                TotalCopies = dto.TotalCopies,
                AvailableCopies = dto.AvailableCopies
            };

            return await _repository.UpdateAsync(book);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}