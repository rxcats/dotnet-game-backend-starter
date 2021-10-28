using System;
using System.Collections.Generic;
using RxCats.GameApi.Conf;
using RxCats.GameApi.Repository.Impl;

namespace RxCats.GameApi.Repository;

public class GameRepository : IDisposable
{
    private readonly GameDatabaseContext _context;

    private readonly Dictionary<string, object> _repositories;

    private bool _disposed;

    public GameRepository(GameDatabaseContext context)
    {
        _context = context;
        _repositories = new Dictionary<string, object>();
    }

    public IUserInfoRepository UserInfo
    {
        get
        {
            const string key = nameof(UserInfoRepository);

            if (!_repositories.ContainsKey(key))
            {
                _repositories.TryAdd(key, new UserInfoRepository(_context));
            }

            return (IUserInfoRepository)_repositories[key];
        }
    }

    public IUserSessionRepository UserSession
    {
        get
        {
            const string key = nameof(UserSessionRepository);

            if (!_repositories.ContainsKey(key))
            {
                _repositories.TryAdd(key, new UserSessionRepository(_context));
            }

            return (IUserSessionRepository)_repositories[key];
        }
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
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