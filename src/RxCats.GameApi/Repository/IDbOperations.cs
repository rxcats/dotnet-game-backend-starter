namespace RxCats.GameApi.Repository;

public interface IDbOperations<TEntity>
{
    Task<TEntity?> FindById(object id);

    Task Insert(TEntity entity);

    Task Delete(object id);

    void Delete(TEntity entityToDelete);

    void Update(TEntity entityToUpdate);

    Task Save();
}