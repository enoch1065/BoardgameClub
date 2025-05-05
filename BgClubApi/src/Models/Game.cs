using System.Text.Json.Serialization;

namespace BgClubApi.Models;

public class Game
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int MinPlayers { get; set; }
    public int MaxPlayers { get; set; }

    public int? BorrowerId { get; set; }
    [JsonIgnore] public Member? Borrower { get; set; }
}