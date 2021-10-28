using MessagePack;

namespace RxCats.GameApiClientTest.WebClient;

[MessagePackObject(true)]
public record struct ApiResponse<T>(int Code, T? Result);