using Community_Library_Management_API.Models;

namespace Community_Library_Management_API.Repositories
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetAllAsync();

        Task<Loan?> GetByIdAsync(int id);

        Task<List<Loan>> GetByMemberIdAsync(int memberId);

        Task<Loan> AddAsync(Loan loan);

        Task<bool> UpdateAsync(Loan loan);
    }
}
