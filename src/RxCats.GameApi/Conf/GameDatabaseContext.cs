using Microsoft.EntityFrameworkCore;
using RxCats.GameApi.Domain.Entity;

namespace RxCats.GameApi.Conf;

public class GameDatabaseContext : DbContext
{
    public DbSet<UserInfo> UserInfo { get; set; } = null!;

    public DbSet<UserGame> UserGame { get; set; } = null!;

    public DbSet<UserSession> UserSession { get; set; } = null!;

    public GameDatabaseContext(DbContextOptions<GameDatabaseContext> options)
        : base(options)
    {
    }

    public override int SaveChanges()
    {
        OnBeforeSaving();
        return base.SaveChanges();
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries();
        var utcNow = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity trackable)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        trackable.UpdatedDateTime = utcNow;
                        entry.Property("CreatedDateTime").IsModified = false;
                        break;
                    case EntityState.Added:
                        trackable.CreatedDateTime = utcNow;
                        trackable.UpdatedDateTime = utcNow;
                        break;
                }
            }
        }
    }
}