using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;
using RxCats.GameApi.Domain;
using RxCats.GameApi.Options;

namespace RxCats.GameApi.Provider.Impl;

public class FirebaseProvider : IAccessTokenValidateProvider
{
    private readonly GameOptions _gameOptions;

    private FirebaseAuth? _firebaseAuth;

    public FirebaseProvider(IOptions<GameOptions> options)
    {
        _gameOptions = options.Value;
    }

    private AppOptions GetOptions()
    {
        var credential = GoogleCredential.FromFile(_gameOptions.FirebaseCredentialFile);
        var options = new AppOptions
        {
            Credential = credential
        };

        return options;
    }

    private void LazySetUpFireBase()
    {
        if (_firebaseAuth != null) return;
        var firebaseApp = FirebaseApp.Create(GetOptions());
        _firebaseAuth = FirebaseAuth.GetAuth(firebaseApp);
    }

    public async Task ValidateAccessToken(string userPlatformId, string accessToken)
    {
        if (!_gameOptions.EnableValidateAccessToken) return;

        LazySetUpFireBase();

        try
        {
            var decoded = await _firebaseAuth!.VerifyIdTokenAsync(accessToken);

            if (decoded.Uid != userPlatformId)
            {
                throw new ServiceException(ResultCode.InvalidAccessToken, "Firebase VerifyIdToken Uid Not Equals.");
            }
        }
        catch
        {
            throw new ServiceException(ResultCode.InvalidAccessToken, "Firebase VerifyIdToken Failure.");
        }
    }
}