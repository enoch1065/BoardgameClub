namespace BgClubApi.Models;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllMembers();

    Task<Member?> GetMemberById(int id);

    Task<bool> MemberExists(int id);

    Task UpdateMember(Member member);

    Task AddMember(Member member);

    Task RemoveMember(Member member);
}