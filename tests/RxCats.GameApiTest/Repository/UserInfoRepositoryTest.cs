using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RxCats.GameApi.Conf;
using RxCats.GameApi.Domain.Entity;
using RxCats.GameApi.Repository;
using RxCats.GameApi.Repository.Impl;

namespace RxCats.GameApiTest.Repository;

[TestClass]
public class UserInfoRepositoryTest
{
    private IUserInfoRepository _repository = null!;
    private GameDatabaseContext _context = null!;

    [TestInitialize]
    public async Task Initialize()
    {
        var gameDatabaseContextFactory = new GameDatabaseContextFactory(TestDatabaseOptions.Create());
        _context = gameDatabaseContextFactory.CreateDbContext();
        _repository = new UserInfoRepository(_context);
        await ClearDb();
    }

    [TestCleanup]
    public void TearDown()
    {
        _context.Dispose();
    }

    private async Task ClearDb()
    {
        await _repository.Delete(-1L);
        await _repository.Delete(-2L);
        await _repository.Delete(-3L);
        await _repository.Delete(-4L);
        await _repository.Delete(-5L);
        await _repository.Save();
    }

    private static UserInfo TestData(long userId)
    {
        var user = new UserInfo
        {
            UserId = userId,
            UserPlatformId = $"test.{userId}",
            Nickname = $"test.{userId}"
        };

        return user;
    }

    [TestMethod]
    public async Task FindByUserPlatformId()
    {
        await _repository.Insert(TestData(-1L));
        await _repository.Save();

        var user = await _repository.FindByUserPlatformId("test.-1");
        Assert.IsNotNull(user);
    }

    [TestMethod]
    public async Task FindById()
    {
        await _repository.Insert(TestData(-2L));
        await _repository.Save();

        var user = (await _repository.FindById(-2L))!;
        Assert.AreEqual("test.-2", user.UserPlatformId);
        Assert.AreEqual("test.-2", user.Nickname);
    }

    [TestMethod]
    public async Task Insert()
    {
        await _repository.Insert(TestData(-3L));
        await _repository.Save();

        var user = _repository.FindById(-3L);
        Assert.IsNotNull(user);
    }

    [TestMethod]
    public async Task Update()
    {
        await _repository.Insert(TestData(-4L));
        await _repository.Save();

        var user = (await _repository.FindById(-4L))!;
        user.Nickname = "test.-4.update";
        _repository.Update(user);
        await _repository.Save();

        var after = (await _repository.FindById(-4L))!;
        Assert.AreEqual("test.-4.update", after.Nickname);
    }

    [TestMethod]
    public async Task Delete()
    {
        await _repository.Insert(TestData(-5L));
        await _repository.Save();

        await _repository.Delete(-5L);
        await _repository.Save();

        var after = await _repository.FindById(-5L);
        Assert.IsNull(after);
    }
}