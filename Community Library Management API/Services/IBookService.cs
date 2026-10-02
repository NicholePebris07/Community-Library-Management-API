using Community_Library_Management_API.Dtos.Books;

namespace Community_Library_Management_API.Services
{
    public interface IBookService
    {
        Task<List<BookReadDto>> GetAllAsync();

        Task<BookReadDto?> GetByIdAsync(int id);

        Task<BookReadDto> CreateAsync(BookCreateDto dto);

        Task<bool> UpdateAsync(int id, BookUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}