using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BgClubApi.Models;

public class GameRepository : IGameRepository
{
    private readonly BgClubApiContext _context;

    public GameRepository(BgClubApiContext context)
        => _context = context;

    public async Task<IEnumerable<Game>> GetAllGames()
        => await _context.Games.ToListAsync();

    public async Task<Game?> GetGameByName(string name)
        => await _context.Games.FirstOrDefaultAsync(g => g.Name == name);

    public async Task AddGame(Game game)
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
    }
}