using RxCats.GameApi.Conf;
using RxCats.GameApi.Domain.Entity;

namespace RxCats.GameApi.Repository.Impl;

public class UserSessionRepository : DbOperations<UserSession>, IUserSessionRepository
{
    public UserSessionRepository(GameDatabaseContext context) : base(context)
    {
    }
}