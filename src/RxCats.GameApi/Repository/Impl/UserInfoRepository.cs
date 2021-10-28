using System.Linq;
using Microsoft.EntityFrameworkCore;
using RxCats.GameApi.Conf;
using RxCats.GameApi.Domain.Entity;

namespace RxCats.GameApi.Repository.Impl;

public class UserInfoRepository : DbOperations<UserInfo>, IUserInfoRepository
{
    public UserInfoRepository(GameDatabaseContext context) : base(context)
    {
    }

    public async Task<UserInfo?> FindByUserPlatformId(string userPlatformId)
    {
        return await DbSet
            .SingleOrDefaultAsync(user => user.UserPlatformId == userPlatformId);
    }
}