using Community_Library_Management_API.Dtos.Members;
using Community_Library_Management_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Community_Library_Management_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MemberReadDto>>> GetAll()
        {
            var members = await _memberService.GetAllAsync();

            return Ok(members);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MemberReadDto>> GetById(int id)
        {
            var member = await _memberService.GetByIdAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult<MemberReadDto>> Create(MemberCreateDto dto)
        {
            var member = await _memberService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = member.Id },
                member);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            MemberUpdateDto dto)
        {
            var updated = await _memberService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var deactivated = await _memberService.DeactivateAsync(id);

            if (!deactivated)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
