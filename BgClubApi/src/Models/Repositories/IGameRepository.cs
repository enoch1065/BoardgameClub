namespace BgClubApi.Models;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetAllGames();

    Task<Game?> GetGameByName(string name);

    Task AddGame(Game game);
}