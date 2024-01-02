using System.Linq.Expressions;

namespace DataAccess.Repositories.interfaces
{
    public interface IGeneralRepository<TEntity>
    {
        ICollection<TEntity> GetAll(string? includeProperties = null);
        TEntity? GetByGuid(Expression<Func<TEntity, bool>> filter, string? includeProperties = null, bool tracked = true);
        TEntity? Create(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);

        void SaveChanges();
    }

}
