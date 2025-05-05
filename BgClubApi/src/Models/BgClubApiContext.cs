using Microsoft.EntityFrameworkCore;

namespace BgClubApi.Models;

public class BgClubApiContext : DbContext
{
    public string DbPath { get; }

    public BgClubApiContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "BgClub.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    public DbSet<Member> Members { get; set; } = null!;

    public DbSet<Game> Games { get; set; } = null!;
}