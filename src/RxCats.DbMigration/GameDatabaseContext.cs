using Microsoft.EntityFrameworkCore;

namespace RxCats.DbMigration;

public class GameDatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            @"Server=localhost;Uid=root;Pwd=qwer1234;Database=game_backend;",
            new MySqlServerVersion("5.7.0"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}