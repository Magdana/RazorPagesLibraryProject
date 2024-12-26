using Microsoft.EntityFrameworkCore;
using RazorPagesLibraryProject.DataAccess;
using RazorPagesLibraryProject.Entities;
using RazorPagesLibraryProject.Repository.Interfaces;
using System.Linq.Expressions;

namespace RazorPagesLibraryProject.Repository.Repositories
{
    public class GenreRepository : GenericRepository<GenreEntity>, IGenreRepository
    {
        private readonly LibraryDbContext _context;
        public GenreRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task Add(GenreEntity entity)
        {
            await base.Add(entity);
        }

        public override async Task<int> SaveChangesAsync()
        {
            var currentTime = DateTime.UtcNow.AddHours(4);

            foreach (var entry in _context.ChangeTracker.Entries().Where(e => e.Entity is BaseEntity))
            {
                var entityBase = (BaseEntity)entry.Entity;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entityBase.CreatedAt ??= currentTime;
                        break;
                    case EntityState.Modified:
                        entityBase.UpdatedAt ??= currentTime;
                        break;
                }
            }
            return await base.SaveChangesAsync();
        }

        public override async Task Update(GenreEntity entity)
        {
            await base.Update(entity);
        }

        public override async Task<GenreEntity?> GetById(int id)
        {
            if (_context == null) return null;
            var entity = await _context.Set<GenreEntity>()
                              .SingleOrDefaultAsync();
            return entity;
        }

        public override IQueryable<GenreEntity> GetAll()
        {
            var entities = _context.Set<GenreEntity>();
            return entities;
        }
        public override async Task Delete(GenreEntity entity)
        {

            await base.Delete(entity);

        }
        public override async Task DeleteRange(IEnumerable<GenreEntity> entities)
        {

            await base.DeleteRange(entities);
        }
        public override async Task<IEnumerable<GenreEntity>> GetAllWhereAsync(Expression<Func<GenreEntity, bool>> predicate)
        {
            var entities = _context.Set<GenreEntity>().Where(predicate).ToListAsync();
            return await entities;
        }
        public override async Task<bool> Any(Expression<Func<GenreEntity, bool>> predicate)
        {
            return await base.Any(predicate);
        }
        public override IQueryable<GenreEntity> GetPagedResult(IQueryable<GenreEntity> entity, int? page, int? count)
        {
            return base.GetPagedResult(entity, page, count);
        }

        public override IQueryable<GenreEntity> GetAllWhere(Expression<Func<GenreEntity, bool>> predicate)
        {
            var entities = _context.Set<GenreEntity>().Where(predicate);
            return entities;
        }

        public override async Task<IEnumerable<GenreEntity>> GetAllAsync()
        {
            var entities = await _context.Set<GenreEntity>().ToListAsync();
            return entities;
        }
    }
}
