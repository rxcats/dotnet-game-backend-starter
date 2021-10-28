namespace RxCats.GameApi.Provider;

public interface IAccessTokenValidateProvider
{
    Task ValidateAccessToken(string userPlatformId, string accessToken);
}