using Microsoft.EntityFrameworkCore;
using RxCats.GameApi.Conf;

namespace RxCats.GameApi.Repository.Impl;

public abstract class DbOperations<TEntity> : IDbOperations<TEntity> where TEntity : class
{
    private readonly GameDatabaseContext _context;

    protected readonly DbSet<TEntity> DbSet;

    protected DbOperations(GameDatabaseContext context)
    {
        _context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> FindById(object id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task Insert(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public virtual async Task Delete(object id)
    {
        var entityToDelete = await DbSet.FindAsync(id);
        if (entityToDelete != null)
        {
            Delete(entityToDelete);
        }
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            DbSet.Attach(entityToDelete);
        }

        DbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        DbSet.Update(entityToUpdate);
    }

    public virtual async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}