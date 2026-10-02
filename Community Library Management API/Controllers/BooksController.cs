using Community_Library_Management_API.Dtos.Books;
using Community_Library_Management_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Community_Library_Management_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookReadDto>>> GetAll()
        {
            var books = await _service.GetAllAsync();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookReadDto>> GetById(int id)
        {
            var book = await _service.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookReadDto>> Create(BookCreateDto dto)
        {
            var book = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = book.Id },
                book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            BookUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}