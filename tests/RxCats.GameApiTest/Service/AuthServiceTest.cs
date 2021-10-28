using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RxCats.GameApi.Conf;
using RxCats.GameApi.Domain.Api;
using RxCats.GameApi.Service;
using RxCats.GameApi.Service.Impl;
using RxCats.GameApi.Web;

namespace RxCats.GameApiTest.Service;

[TestClass]
public class AuthServiceTest
{
    private IAuthService _service = null!;

    [TestInitialize]
    public void Initialize()
    {
        var gameDatabaseContextFactory = new GameDatabaseContextFactory(TestDatabaseOptions.Create());
        _service = new AuthService(gameDatabaseContextFactory);
    }

    [TestCleanup]
    public void Cleanup()
    {

    }

    [TestMethod]
    public async Task Login()
    {
        var request = new LoginRequest
        {
            UserPlatformId = "1.test",
            AccessToken = "AccessToken"
        };

        var user = await _service.Login(request, new HttpMetaData());
        Assert.IsTrue(user.UserId > 0);
    }
}