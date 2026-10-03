using Community_Library_Management_API.Dtos.Loans;
using Community_Library_Management_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Community_Library_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LoanReadDto>>> GetAll()
        {
            var loans = await _loanService.GetAllAsync();

            return Ok(loans);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LoanReadDto>> GetById(int id)
        {
            var loan = await _loanService.GetByIdAsync(id);

            if (loan == null)
            {
                return NotFound();
            }

            return Ok(loan);
        }

        [HttpPost]
        public async Task<ActionResult<LoanReadDto>> Borrow(LoanCreateDto dto)
        {
            try
            {
                var loan = await _loanService.BorrowAsync(dto);

                if (loan == null)
                {
                    return NotFound(
                        "The specified book or member does not exist.");
                }

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = loan.Id },
                    loan);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("{id:int}/return")]
        public async Task<ActionResult<LoanReadDto>> Return(int id)
        {
            try
            {
                var returned = await _loanService.ReturnAsync(id);

                if (!returned)
                {
                    return NotFound();
                }

                var loan = await _loanService.GetByIdAsync(id);

                return Ok(loan);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
