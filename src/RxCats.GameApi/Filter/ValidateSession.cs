using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RxCats.GameApi.Conf;
using RxCats.GameApi.Domain;
using RxCats.GameApi.Extensions;
using RxCats.GameApi.Options;
using RxCats.GameApi.Repository;

namespace RxCats.GameApi.Filter;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ValidateSession : ActionFilterAttribute
{
    private readonly ILogger<ValidateSession> _logger;
    private readonly GameOptions _gameOptions;
    private readonly IDbContextFactory<GameDatabaseContext> _dbContextFactory;

    public ValidateSession(ILogger<ValidateSession> logger, IOptions<GameOptions> gameOptions,
        IDbContextFactory<GameDatabaseContext> dbContextFactory)
    {
        _logger = logger;
        _gameOptions = gameOptions.Value;
        _dbContextFactory = dbContextFactory;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userIdString = context.HttpContext.Request.Headers["X-UserId"].FirstOrDefault();

        var userId = string.IsNullOrWhiteSpace(userIdString) ? 0 : long.Parse(userIdString);

        var accessToken = context.HttpContext.Request.Headers["X-AccessToken"].FirstOrDefault() ?? string.Empty;

        var path = context.HttpContext.Request.Path;

        var ip = context.HttpContext.GetClientIp();

        await ValidateAccessToken(userId, accessToken, path.ToString(), ip);

        await next();
    }

    private async Task ValidateAccessToken(long userId, string accessToken, string path, string ip)
    {
        if (userId == 0 || accessToken == string.Empty)
        {
            throw new ServiceException(ResultCode.InvalidAccessToken,
                "Required Headers `X-UserId` And `X-AccessToken`");
        }

        if (!_gameOptions.EnableValidateAccessToken)
        {
            return;
        }

        using var context = new GameRepository(_dbContextFactory.CreateDbContext());

        var session = await context.UserSession.FindById(userId) ??
                      throw new ServiceException(ResultCode.UnAuthorized, "UserSession Could Not Found.");

        if (!session.IsValid(accessToken))
        {
            throw new ServiceException(ResultCode.InvalidAccessToken, "Invalid AccessToken.");
        }

        if (session.IsExpired())
        {
            throw new ServiceException(ResultCode.InvalidAccessToken, "Expired AccessToken.");
        }

        session.ApiPath = path;
        session.IpAddress = ip;
        session.UpdateExpiryDateTime();

        context.UserSession.Update(session);
        await context.Save();
    }
}