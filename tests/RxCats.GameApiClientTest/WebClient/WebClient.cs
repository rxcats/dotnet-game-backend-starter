using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MessagePack;

namespace RxCats.GameApiClientTest.WebClient;

public class WebClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private const string BaseUri = "http://localhost:11000";
    private bool _disposed;

    public WebClient()
    {
        _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(10)
        };
    }

    public async Task<T?> SendPostAsync<T>(string api, object request, Dictionary<string, string>? headers = null)
    {
        var req = ToByteArrayContent(request);
        req.Headers.ContentType = new MediaTypeHeaderValue("application/x-msgpack");

        if (headers != null)
        {
            foreach (var header in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        try
        {
            using var response = await _httpClient.PostAsync($"{BaseUri}/{api.TrimStart('/')}", req);
            var bytes = await response.Content.ReadAsByteArrayAsync();
            Console.WriteLine($"StatusCode : {response.StatusCode.ToString()} ({(int)response.StatusCode})");
            Console.WriteLine($"ResponseBody : {MessagePackSerializer.ConvertToJson(bytes)}");
            return MessagePackSerializer.Deserialize<T>(bytes);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e.Message);
            Console.Error.WriteLine(e.StackTrace);
        }

        return default(T?);
    }

    private static ByteArrayContent ToByteArrayContent(object request)
    {
        var reqBytes = MessagePackSerializer.Serialize(request);
        var reqBody = new ByteArrayContent(reqBytes);
        return reqBody;
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _httpClient.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}