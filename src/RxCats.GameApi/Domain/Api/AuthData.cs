using MessagePack;

namespace RxCats.GameApi.Domain.Api;

[MessagePackObject(true)]
public record struct LoginResponse(long UserId);

[MessagePackObject(true)]
public record struct LoginRequest
(
    string UserPlatformId,
    string AccessToken,
    string? Nickname,
    string? PhotoUrl,
    string? ProviderId,
    string? ProviderUserId,
    string? ProviderName,
    string? ProviderEmail
);