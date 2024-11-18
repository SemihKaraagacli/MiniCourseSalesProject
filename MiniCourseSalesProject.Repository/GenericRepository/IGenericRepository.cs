using System.Linq.Expressions;

namespace MiniCourseSalesProject.Repository.GenericRepository
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate);
    }
}
