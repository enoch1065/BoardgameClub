using Microsoft.EntityFrameworkCore;

namespace BgClubApi.Models;

public class BgClubApiContext : DbContext
{
    public BgClubApiContext(DbContextOptions<BgClubApiContext> options)
        : base(options)
    {
    }

    public DbSet<Member> Members { get; set; } = null!;

    public DbSet<Game> Games { get; set; } = null!;
}