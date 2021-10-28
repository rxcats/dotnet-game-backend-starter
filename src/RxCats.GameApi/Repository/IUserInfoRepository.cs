using RxCats.GameApi.Domain.Entity;

namespace RxCats.GameApi.Repository;

public interface IUserInfoRepository : IDbOperations<UserInfo>
{
    Task<UserInfo?> FindByUserPlatformId(string userPlatformId);
}