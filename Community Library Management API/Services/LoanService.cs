using Community_Library_Management_API.Dtos.Loans;
using Community_Library_Management_API.Models;
using Community_Library_Management_API.Repositories;

namespace Community_Library_Management_API.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;

        public LoanService(
            ILoanRepository loanRepository,
            IBookRepository bookRepository,
            IMemberRepository memberRepository)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }

        public async Task<List<LoanReadDto>> GetAllAsync()
        {
            var loans = await _loanRepository.GetAllAsync();

            return loans.Select(MapToDto).ToList();
        }

        public async Task<LoanReadDto?> GetByIdAsync(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);

            if (loan == null)
            {
                return null;
            }

            return MapToDto(loan);
        }

        public async Task<LoanReadDto?> BorrowAsync(LoanCreateDto dto)
        {
            var book = await _bookRepository.GetByIdAsync(dto.BookId);

            if (book == null)
            {
                return null;
            }

            var member = await _memberRepository.GetByIdAsync(dto.MemberId);

            if (member == null)
            {
                return null;
            }

            if (!member.IsActive)
            {
                throw new InvalidOperationException(
                    "Inactive members cannot borrow books.");
            }

            if (book.AvailableCopies <= 0)
            {
                throw new InvalidOperationException(
                    "The book has no available copies.");
            }

            var memberLoans = await _loanRepository
                .GetByMemberIdAsync(dto.MemberId);

            var activeLoanCount = memberLoans.Count(l =>
                l.Status == "Borrowed" || l.Status == "Overdue");

            if (activeLoanCount >= 3)
            {
                throw new InvalidOperationException(
                    "A member cannot have more than 3 active loans.");
            }

            var borrowedDate = DateTime.UtcNow;

            var loan = new Loan
            {
                BookId = book.Id,
                MemberId = member.Id,
                BorrowedDate = borrowedDate,
                DueDate = borrowedDate.AddDays(7),
                ReturnedDate = null,
                Status = "Borrowed"
            };

            book.AvailableCopies--;

            var createdLoan = await _loanRepository.AddAsync(loan);

            return await GetByIdAsync(createdLoan.Id);
        }

        public async Task<bool> ReturnAsync(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);

            if (loan == null)
            {
                return false;
            }

            if (loan.ReturnedDate != null || loan.Status == "Returned")
            {
                throw new InvalidOperationException(
                    "This loan has already been returned.");
            }

            loan.ReturnedDate = DateTime.UtcNow;
            loan.Status = "Returned";

            loan.Book.AvailableCopies++;

            return await _loanRepository.UpdateAsync(loan);
        }

        public async Task<List<LoanReadDto>?> GetByMemberIdAsync(int memberId)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);

            if (member == null)
            {
                return null;
            }

            var loans = await _loanRepository.GetByMemberIdAsync(memberId);

            return loans.Select(MapToDto).ToList();
        }

        private static LoanReadDto MapToDto(Loan loan)
        {
            return new LoanReadDto
            {
                Id = loan.Id,
                BookId = loan.BookId,
                BookTitle = loan.Book.Title,
                MemberId = loan.MemberId,
                MemberName = loan.Member.FullName,
                BorrowedDate = loan.BorrowedDate,
                DueDate = loan.DueDate,
                ReturnedDate = loan.ReturnedDate,
                Status = loan.Status
            };
        }
    }
}
