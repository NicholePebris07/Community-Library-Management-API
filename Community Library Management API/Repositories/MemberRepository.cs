using Community_Library_Management_API.Data;
using Community_Library_Management_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Community_Library_Management_API.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryDbContext _context;

        public MemberRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Member>> GetAllAsync()
        {
            return await _context.Members
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Member?> GetByIdAsync(int id)
        {
            return await _context.Members
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Member> AddAsync(Member member)
        {
            _context.Members.Add(member);

            await _context.SaveChangesAsync();

            return member;
        }

        public async Task<bool> UpdateAsync(Member member)
        {
            var existingMember = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == member.Id);

            if (existingMember == null)
            {
                return false;
            }

            existingMember.FullName = member.FullName;
            existingMember.Email = member.Email;
            existingMember.MembershipType = member.MembershipType;
            existingMember.IsActive = member.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);

            if (member == null)
            {
                return false;
            }

            member.IsActive = false;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
