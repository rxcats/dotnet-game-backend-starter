using RxCats.GameApi.Domain.Api;
using RxCats.GameApi.Domain.Entity;
using RxCats.GameApi.Web;

namespace RxCats.GameApi.Service;

public interface IAuthService
{
    Task<UserInfo> Login(LoginRequest request, HttpMetaData httpMetaData);
}