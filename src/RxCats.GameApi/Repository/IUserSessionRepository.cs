using RxCats.GameApi.Domain.Entity;

namespace RxCats.GameApi.Repository;

public interface IUserSessionRepository : IDbOperations<UserSession>
{
}