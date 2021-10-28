namespace RxCats.GameApi.Domain;

public enum ResultCode
{
    Ok = 0,
    InternalServerError = 90001,
    UnKnownUser = 91001,
    InvalidAccessToken = 91002,
    UnAuthorized = 91003,
}