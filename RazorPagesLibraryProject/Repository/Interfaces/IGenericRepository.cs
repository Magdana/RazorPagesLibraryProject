using System.Linq.Expressions;

namespace RazorPagesLibraryProject.Repository.Interfaces
{
        public interface IGenericRepository<T> where T : class
        {
            Task<bool> Any(Expression<Func<T, bool>> predicate);

            Task<T?> GetById(int Id);

            Task<IEnumerable<T>> GetAllAsync();

            Task<IEnumerable<T>> GetAllWhereAsync(Expression<Func<T, bool>> predicate);

            IQueryable<T> GetAll();
            IQueryable<T> GetAllWhere(Expression<Func<T, bool>> predicate);

            IQueryable<T> GetPagedResult(IQueryable<T> entity, int? page, int? count);

            Task Add(T entity);

            Task Update(T entity);

            Task Delete(T entity);
            Task DeleteRange(IEnumerable<T> entity);

            Task<int> SaveChangesAsync();


        }
}
