using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RxCats.GameApiClientTest.WebClient;

namespace RxCats.GameApiClientTest.Api;

[Ignore]
[TestClass]
public class AuthApiTest
{
    private WebClient.WebClient _webClient = null!;

    [TestInitialize]
    public void Initialize()
    {
        _webClient = new WebClient.WebClient();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _webClient.Dispose();
    }

    [TestMethod]
    public async Task Test()
    {
        var response = await _webClient.SendPostAsync<ApiResponse<LoginResponse>>("/Auth/Login", new LoginRequest
        {
            UserPlatformId = "1.test",
            AccessToken = "AccessToken"
        });

        Console.WriteLine(response);

        Assert.IsTrue(response.Result.UserId > 0);
    }
}