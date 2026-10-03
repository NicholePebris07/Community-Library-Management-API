using Community_Library_Management_API.Dtos.Members;
using Community_Library_Management_API.Models;
using Community_Library_Management_API.Repositories;

namespace Community_Library_Management_API.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<List<MemberReadDto>> GetAllAsync()
        {
            var members = await _memberRepository.GetAllAsync();

            return members.Select(MapToDto).ToList();
        }

        public async Task<MemberReadDto?> GetByIdAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);

            if (member == null)
            {
                return null;
            }

            return MapToDto(member);
        }

        public async Task<MemberReadDto> CreateAsync(MemberCreateDto dto)
        {
            var member = new Member
            {
                FullName = dto.FullName,
                Email = dto.Email,
                MembershipType = dto.MembershipType,
                DateJoined = DateTime.UtcNow,
                IsActive = true
            };

            var createdMember = await _memberRepository.AddAsync(member);

            return MapToDto(createdMember);
        }

        public async Task<bool> UpdateAsync(int id, MemberUpdateDto dto)
        {
            var member = new Member
            {
                Id = id,
                FullName = dto.FullName,
                Email = dto.Email,
                MembershipType = dto.MembershipType,
                IsActive = dto.IsActive
            };

            return await _memberRepository.UpdateAsync(member);
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            return await _memberRepository.DeactivateAsync(id);
        }

        private static MemberReadDto MapToDto(Member member)
        {
            return new MemberReadDto
            {
                Id = member.Id,
                FullName = member.FullName,
                Email = member.Email,
                MembershipType = member.MembershipType,
                DateJoined = member.DateJoined,
                IsActive = member.IsActive
            };
        }
    }
}
