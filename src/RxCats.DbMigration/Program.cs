using Microsoft.EntityFrameworkCore;
using RxCats.DbMigration;

using var context = new GameDatabaseContext();
context.Database.Migrate();