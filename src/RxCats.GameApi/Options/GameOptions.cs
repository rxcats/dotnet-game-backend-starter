namespace RxCats.GameApi.Options;

public record GameOptions
{
    public bool EnableValidateAccessToken { get; set; }
    public string FirebaseCredentialFile { get; set; }
}