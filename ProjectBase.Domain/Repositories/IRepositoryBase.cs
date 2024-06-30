using ProjectBase.Domain.Pagination;
using System.Linq.Expressions;

namespace ProjectBase.Domain.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(ICollection<TEntity> entity);
        Task AddRange(ICollection<TEntity> entity);
        Task<PageList<TEntity>> GetAll(int pageSize, int pageIndex);
        Task<PageList<TEntity>> GetSortedList(int pageSize, 
                                                int pageIndex, 
                                                string sortString = null,
                                                bool isAscending = true);
        Task<TEntity?> GetByCondition(Expression<Func<TEntity, bool>> predicate, 
                                                bool trackChange = false);
        Task<List<TEntity>> GetListByCondition(Expression<Func<TEntity, bool>> predicate,
                                                int pageIndex,
                                                int pageSize,
                                                bool trackChange = false);
        void ExplicitLoad<TProperty>(TEntity entity, 
                                     Expression<Func<TEntity, TProperty?>> navigationProperty) 
                                        where TProperty : class;
        void ExplicitLoadCollection<TProperty>(TEntity entity,
                                     Expression<Func<TEntity, IEnumerable<TProperty>?>> navigationProperty)
                                        where TProperty : class;

    }
}
