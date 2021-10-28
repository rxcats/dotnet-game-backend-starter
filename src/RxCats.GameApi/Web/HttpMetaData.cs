using RxCats.GameApi.Extensions;

namespace RxCats.GameApi.Web;

public readonly struct HttpMetaData
{
    private readonly HttpContext? _context;

    public HttpMetaData(HttpContext? context = null)
    {
        _context = context;
    }

    public string ApiPath => _context == null ? string.Empty : _context.GetApiPath();

    public string IpAddress => _context == null ? string.Empty : _context.GetClientIp();
}