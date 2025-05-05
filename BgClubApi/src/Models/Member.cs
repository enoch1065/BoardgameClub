namespace BgClubApi.Models;

public class Member
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsActive { get; set; }
    public List<Game> BorrowedGames { get; } = new();
}