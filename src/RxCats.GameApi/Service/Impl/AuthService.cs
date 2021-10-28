using Microsoft.EntityFrameworkCore;
using RxCats.GameApi.Conf;
using RxCats.GameApi.Domain.Api;
using RxCats.GameApi.Domain.Entity;
using RxCats.GameApi.Repository;
using RxCats.GameApi.Web;

namespace RxCats.GameApi.Service.Impl;

public class AuthService : IAuthService
{
    private readonly IDbContextFactory<GameDatabaseContext> _dbContextFactory;

    public AuthService(IDbContextFactory<GameDatabaseContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    private static UserInfo CreateUserInfo(LoginRequest request)
    {
        return new UserInfo
        {
            UserPlatformId = request.UserPlatformId,
            Nickname = request.Nickname ?? string.Empty,
            PhotoUrl = request.PhotoUrl ?? string.Empty,
            ProviderId = request.ProviderId ?? string.Empty,
            ProviderUserId = request.ProviderUserId ?? string.Empty,
            ProviderName = request.ProviderName ?? string.Empty,
            ProviderEmail = request.ProviderEmail ?? string.Empty,
        };
    }

    private static UserInfo UpdateUserInfo(UserInfo user, LoginRequest request)
    {
        if (!string.IsNullOrEmpty(request.ProviderId) && user.ProviderId != request.ProviderId)
        {
            user.ProviderId = request.ProviderId;
        }

        if (!string.IsNullOrEmpty(request.ProviderUserId) && user.ProviderUserId != request.ProviderUserId)
        {
            user.ProviderUserId = request.ProviderUserId;
        }

        if (!string.IsNullOrEmpty(request.ProviderName) && user.ProviderName != request.ProviderName)
        {
            user.ProviderName = request.ProviderName;
        }

        if (!string.IsNullOrEmpty(request.ProviderEmail) && user.ProviderEmail != request.ProviderEmail)
        {
            user.ProviderEmail = request.ProviderEmail;
        }

        return user;
    }

    private static UserSession CreateUserSession(long userId, string accessToken, HttpMetaData httpMetaData)
    {
        var session = new UserSession
        {
            UserId = userId,
            AccessToken = accessToken,
            ApiPath = httpMetaData.ApiPath,
            IpAddress = httpMetaData.IpAddress
        };

        session.UpdateExpiryDateTime();

        return session;
    }

    private static UserSession UpdateUserSession(UserSession session, string accessToken, HttpMetaData httpMetaData)
    {
        session.AccessToken = accessToken;
        session.ApiPath = httpMetaData.ApiPath;
        session.IpAddress = httpMetaData.IpAddress;
        session.UpdateExpiryDateTime();
        return session;
    }

    public async Task<UserInfo> Login(LoginRequest request, HttpMetaData httpMetaData)
    {
        using var context = new GameRepository(_dbContextFactory.CreateDbContext());

        var user = await context.UserInfo.FindByUserPlatformId(request.UserPlatformId);

        if (user == null)
        {
            user = CreateUserInfo(request);

            await context.UserInfo.Insert(user);

            await context.Save();
        }
        else
        {
            user = UpdateUserInfo(user, request);

            context.UserInfo.Update(user);
        }

        var session = await context.UserSession.FindById(user.UserId);

        if (session == null)
        {
            session = CreateUserSession(user.UserId, request.AccessToken, httpMetaData);

            await context.UserSession.Insert(session);
        }
        else
        {
            session = UpdateUserSession(session, request.AccessToken, httpMetaData);

            context.UserSession.Update(session);
        }

        await context.Save();

        return user;
    }
}