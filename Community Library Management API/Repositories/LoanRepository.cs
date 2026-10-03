using Community_Library_Management_API.Data;
using Community_Library_Management_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Community_Library_Management_API.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _context;

        public LoanRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await _context.Loans
                .AsNoTracking()
                .Include(l => l.Book)
                .Include(l => l.Member)
                .ToListAsync();
        }

        public async Task<Loan?> GetByIdAsync(int id)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<Loan>> GetByMemberIdAsync(int memberId)
        {
            return await _context.Loans
                .AsNoTracking()
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l => l.MemberId == memberId)
                .ToListAsync();
        }

        public async Task<Loan> AddAsync(Loan loan)
        {
            _context.Loans.Add(loan);

            await _context.SaveChangesAsync();

            return loan;
        }

        public async Task<bool> UpdateAsync(Loan loan)
        {
            var existingLoan = await _context.Loans
                .FirstOrDefaultAsync(l => l.Id == loan.Id);

            if (existingLoan == null)
            {
                return false;
            }

            existingLoan.ReturnedDate = loan.ReturnedDate;
            existingLoan.Status = loan.Status;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
