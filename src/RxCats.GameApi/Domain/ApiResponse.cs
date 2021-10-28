using MessagePack;

namespace RxCats.GameApi.Domain;

[MessagePackObject(true)]
public record struct ApiResponse<T>(int Code, T? Result);