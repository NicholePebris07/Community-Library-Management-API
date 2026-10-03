using Community_Library_Management_API.Models;

namespace Community_Library_Management_API.Repositories
{
    public interface IMemberRepository
    {
        Task<List<Member>> GetAllAsync();

        Task<Member?> GetByIdAsync(int id);

        Task<Member> AddAsync(Member member);

        Task<bool> UpdateAsync(Member member);

        Task<bool> DeactivateAsync(int id);
    }
}
