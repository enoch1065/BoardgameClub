using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BgClubApi.Models;

public class MemberRepository : IMemberRepository
{
    private readonly BgClubApiContext _context;

    public MemberRepository(BgClubApiContext context)
        => _context = context;

    public async Task<IEnumerable<Member>> GetAllMembers()
        => await _context.Members.ToListAsync();

    public async Task<Member> GetMemberById(int id)
        => await _context.Members.FindAsync(id);

    public async Task<bool> MemberExists(int id)
        => await _context.Members.AnyAsync(e => e.Id == id);

    public async Task UpdateMember(Member member)
    {
        _context.Entry(member).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task AddMember(Member member)
    {
        _context.Members.Add(member);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveMember(Member member)
    {
        _context.Members.Remove(member);
        await _context.SaveChangesAsync();
    }
}