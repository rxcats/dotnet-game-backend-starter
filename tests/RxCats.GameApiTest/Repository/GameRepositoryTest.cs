using Microsoft.VisualStudio.TestTools.UnitTesting;
using RxCats.GameApi.Conf;
using RxCats.GameApi.Repository;

namespace RxCats.GameApiTest.Repository;

[TestClass]
public class GameRepositoryTest
{
    private GameRepository _repository = null!;

    [TestInitialize]
    public void Initialize()
    {
        var gameDatabaseContextFactory = new GameDatabaseContextFactory(TestDatabaseOptions.Create());
        _repository = new GameRepository(gameDatabaseContextFactory.CreateDbContext());
    }

    [TestCleanup]
    public void Cleanup()
    {
        _repository.Dispose();
    }

    [TestMethod]
    public void Test()
    {

    }
}