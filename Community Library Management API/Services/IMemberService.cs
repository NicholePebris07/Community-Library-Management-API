using Community_Library_Management_API.Dtos.Members;

namespace Community_Library_Management_API.Services
{
    public interface IMemberService
    {
        Task<List<MemberReadDto>> GetAllAsync();

        Task<MemberReadDto?> GetByIdAsync(int id);

        Task<MemberReadDto> CreateAsync(MemberCreateDto dto);

        Task<bool> UpdateAsync(int id, MemberUpdateDto dto);

        Task<bool> DeactivateAsync(int id);
    }
}
