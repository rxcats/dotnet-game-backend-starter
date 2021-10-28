using System;
using Microsoft.EntityFrameworkCore;
using RxCats.GameApi.Conf;

namespace RxCats.GameApiTest;

public static class TestDatabaseOptions
{
    public static DbContextOptions<GameDatabaseContext> Create()
    {
        return new DbContextOptionsBuilder<GameDatabaseContext>()
            .UseMySql(@"Server=localhost;Uid=root;Pwd=qwer1234;Database=test;", new MySqlServerVersion("5.7.0"))
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .LogTo(Console.WriteLine)
            .Options;
    }
}