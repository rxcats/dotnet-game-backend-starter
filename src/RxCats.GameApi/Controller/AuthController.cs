using Microsoft.AspNetCore.Mvc;
using RxCats.GameApi.Domain;
using RxCats.GameApi.Domain.Api;
using RxCats.GameApi.Provider;
using RxCats.GameApi.Service;
using RxCats.GameApi.Web;

namespace RxCats.GameApi.Controller;

[Consumes(MediaType.MessagePack)]
[Produces(MediaType.MessagePack)]
[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IAccessTokenValidateProvider _validateProvider;

    public AuthController(IAuthService authService,
        IAccessTokenValidateProvider validateProvider)
    {
        _authService = authService;
        _validateProvider = validateProvider;
    }

    [HttpPost]
    public async Task<ApiResponse<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        await _validateProvider.ValidateAccessToken(request.UserPlatformId, request.AccessToken);

        var user = await _authService.Login(request, new HttpMetaData(HttpContext));

        return new ApiResponse<LoginResponse>
        {
            Result = new LoginResponse(user.UserId)
        };
    }
}