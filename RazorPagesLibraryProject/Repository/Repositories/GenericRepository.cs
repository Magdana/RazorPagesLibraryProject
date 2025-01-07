using Microsoft.EntityFrameworkCore;
using RazorPagesLibraryProject.DataAccess;
using RazorPagesLibraryProject.Repository.Interfaces;
using System.Linq.Expressions;

namespace RazorPagesLibraryProject.Repository.Repositories
{
        public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
            protected readonly LibraryDbContext _context;
            public GenericRepository(LibraryDbContext context)
            {
                _context = context;
            }
            public virtual async Task<bool> Any(Expression<Func<T, bool>> predicate) => await _context.Set<T>().AnyAsync(predicate);

            public virtual async Task<T?> GetById(int Id) => await _context.Set<T>().FindAsync(Id);

            public virtual async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
            public virtual async Task<IEnumerable<T>> GetAllWhereAsync(Expression<Func<T, bool>> predicate) => await _context.Set<T>().Where(predicate).ToListAsync();

            public virtual IQueryable<T> GetAll() => _context.Set<T>();
            public virtual IQueryable<T> GetAllWhere(Expression<Func<T, bool>> predicate) => _context.Set<T>().Where(predicate);

            public virtual IQueryable<T> GetPagedResult(IQueryable<T> entity, int? page, int? count)
            {
                if (page < 0) page = 0;
                var countValue = count ?? 10;
                var pageValue = (page.HasValue && page > 0) ? (page.Value - 1) * countValue : 0;
                return entity.Skip(pageValue).Take(countValue);
            }

            public virtual async Task Add(T entity)
            {
                if (entity == null) return;
                await _context.Set<T>().AddAsync(entity);
                await this.SaveChangesAsync();
            }
            public virtual async Task Update(T entity)
            {
                if (entity == null) return;
                _context.Set<T>().Update(entity);
                await this.SaveChangesAsync();
            }

            public virtual async Task Delete(T entity)
            {
                if (entity == null) return;
                _context.Set<T>().Remove(entity);
                await this.SaveChangesAsync();
            }
            public virtual async Task DeleteRange(IEnumerable<T> entities)
            {
                if (entities == null || !entities.Any()) return;

                foreach (var entity in entities)
                {
                    if (entity != null)
                    {
                        _context.Entry(entity).State = EntityState.Deleted;
                    }
                }
                await this.SaveChangesAsync();
            }

            public virtual async Task<int> SaveChangesAsync()
            {
                return await _context.SaveChangesAsync();
            }

        }
    }
