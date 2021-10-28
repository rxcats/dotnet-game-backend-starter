namespace RxCats.GameApi.Domain.Dto;

public readonly record struct ApiLogData(
    string Method,
    string Uri,
    int Status,
    string ClientIp,
    Dictionary<string, List<string>> RequestHeaders,
    string RequestBody,
    string ResponseBody
)
{
    public string RequestHeadersToString => string.Join(",",
        RequestHeaders.Select(pair =>
            "{" + pair.Key + "=" + "[" + pair.Value.Aggregate((x, y) => x + "," + y) + "]}"));
}