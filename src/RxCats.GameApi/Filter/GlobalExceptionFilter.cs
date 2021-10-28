using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RxCats.GameApi.Domain;
using RxCats.GameApi.Web;
using ZLogger;

namespace RxCats.GameApi.Filter;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.ZLogError("{0}", context.Exception.Message);
        _logger.ZLogError("{0}", context.Exception.StackTrace);

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        switch (context.HttpContext.Request.ContentType)
        {
            case MediaType.MessagePack:
            {
                context.HttpContext.Response.ContentType = MediaType.MessagePack;
                CreateErrorResult(context);
                break;
            }
            case MediaTypeNames.Application.Json:
            {
                context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
                CreateErrorResult(context);
                break;
            }
            default:
            {
                context.HttpContext.Response.ContentType = MediaTypeNames.Text.Plain;
                CreateTextErrorResult(context);
                break;
            }
        }
    }

    private void CreateErrorResult(ExceptionContext context)
    {
        int code;

        if (context.Exception is ServiceException e)
        {
            code = (int)e.ResultCode;
        }
        else
        {
            code = (int)ResultCode.InternalServerError;
        }

        context.Result = new ObjectResult(new ApiResponse<string>
        {
            Code = code,
            Result = context.Exception.Message
        });
    }

    private void CreateTextErrorResult(ExceptionContext context)
    {
        context.Result = new ObjectResult(context.Exception.Message);
    }
}