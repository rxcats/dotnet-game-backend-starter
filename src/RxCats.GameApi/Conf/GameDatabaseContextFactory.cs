using Microsoft.EntityFrameworkCore;

namespace RxCats.GameApi.Conf;

public class GameDatabaseContextFactory : IDbContextFactory<GameDatabaseContext>
{
    private readonly DbContextOptions<GameDatabaseContext> _options;

    public GameDatabaseContextFactory(DbContextOptions<GameDatabaseContext> options)
    {
        _options = options;
    }

    public GameDatabaseContext CreateDbContext()
    {
        return new GameDatabaseContext(_options);
    }
}