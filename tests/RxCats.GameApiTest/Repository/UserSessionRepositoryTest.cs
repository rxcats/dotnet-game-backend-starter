using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RxCats.GameApi.Conf;
using RxCats.GameApi.Domain.Entity;
using RxCats.GameApi.Repository;
using RxCats.GameApi.Repository.Impl;

namespace RxCats.GameApiTest.Repository;

[TestClass]
public class UserSessionRepositoryTest
{
    private IUserSessionRepository _repository = null!;
    private GameDatabaseContext _context = null!;

    [TestInitialize]
    public async Task Initialize()
    {
        var gameDatabaseContextFactory = new GameDatabaseContextFactory(TestDatabaseOptions.Create());
        _context = gameDatabaseContextFactory.CreateDbContext();
        _repository = new UserSessionRepository(_context);
        await ClearDb();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
    }

    private async Task ClearDb()
    {
        await _repository.Delete(-1L);
        await _repository.Delete(-2L);
        await _repository.Delete(-3L);
        await _repository.Delete(-4L);
        await _repository.Save();
    }

    private static UserSession TestData(long userId)
    {
        var session = new UserSession
        {
            UserId = userId,
            AccessToken = "AccessToken",
            ApiPath = "/test",
            IpAddress = "127.0.0.1"
        };

        session.UpdateExpiryDateTime();

        return session;
    }

    [TestMethod]
    public async Task FindById()
    {
        await _repository.Insert(TestData(-1L));
        await _repository.Save();

        var session = await _repository.FindById(-1L);
        Assert.AreEqual("AccessToken", session!.AccessToken);
    }

    [TestMethod]
    public async Task Insert()
    {
        await _repository.Insert(TestData(-2L));
        await _repository.Save();

        var after = await _repository.FindById(-2L);
        Assert.IsNotNull(after);
    }

    [TestMethod]
    public async Task Update()
    {
        await _repository.Insert(TestData(-3L));
        await _repository.Save();

        var session = (await _repository.FindById(-3L))!;
        session.AccessToken = "AccessTokenUpdate";
        session.UpdateExpiryDateTime();
        _repository.Update(session);
        await _repository.Save();

        var after = (await _repository.FindById(-3L))!;
        Assert.AreEqual("AccessTokenUpdate", after.AccessToken);
    }

    [TestMethod]
    public async Task Delete()
    {
        await _repository.Insert(TestData(-4L));
        await _repository.Save();

        await _repository.Delete(-4L);
        await _repository.Save();

        var after = await _repository.FindById(-4L);
        Assert.IsNull(after);
    }
}