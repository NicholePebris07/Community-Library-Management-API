using Community_Library_Management_API.Dtos.Loans;

namespace Community_Library_Management_API.Services
{
    public interface ILoanService
    {
        Task<List<LoanReadDto>> GetAllAsync();

        Task<LoanReadDto?> GetByIdAsync(int id);

        Task<LoanReadDto?> BorrowAsync(LoanCreateDto dto);

        Task<bool> ReturnAsync(int id);

        Task<List<LoanReadDto>?> GetByMemberIdAsync(int memberId);
    }
}
