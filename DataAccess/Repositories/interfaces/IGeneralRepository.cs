namespace DataAccess.Repositories.interfaces
{
    public interface IGeneralRepository<TEntity>
    {
        ICollection<TEntity> GetAll(string? includeProperties = null);
        TEntity? GetByGuid(Guid guid);
        TEntity? Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        bool IsExist(Guid guid);

        void SaveChanges();
    }

}
