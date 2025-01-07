using Microsoft.EntityFrameworkCore;
using RazorPagesLibraryProject.DataAccess;
using RazorPagesLibraryProject.Entities;
using RazorPagesLibraryProject.Repository.Interfaces;
using System.Linq.Expressions;
using static RazorPagesLibraryProject.Repository.Repositories.BookRepository;

namespace RazorPagesLibraryProject.Repository.Repositories
{
    public class BookRepository : GenericRepository<BookEntity>, IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }
        public override async Task Add(BookEntity entity)
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

        public override async Task Update(BookEntity entity)
        {
            await base.Update(entity);
        }

        public override async Task<BookEntity?> GetById(int id)
        {
            if (_context == null) return null;
            var entity = await _context.Set<BookEntity>()
                .Include(b => b.Genre)
                              .FirstOrDefaultAsync(book => book.Id == id);
            return entity;
        }

        public override IQueryable<BookEntity> GetAll()
        {
            var entities = _context.Set<BookEntity>();
            return entities;
        }
        public override async Task Delete(BookEntity entity)
        {

            await base.Delete(entity);

        }
        public override async Task DeleteRange(IEnumerable<BookEntity> entities)
        {

            await base.DeleteRange(entities);
        }
        public override async Task<IEnumerable<BookEntity>> GetAllWhereAsync(Expression<Func<BookEntity, bool>> predicate)
        {
            var entities = _context.Set<BookEntity>().Where(predicate).ToListAsync();
            return await entities;
        }
        public override async Task<bool> Any(Expression<Func<BookEntity, bool>> predicate)
        {
            return await base.Any(predicate);
        }
        public override IQueryable<BookEntity> GetPagedResult(IQueryable<BookEntity> entity, int? page, int? count)
        {
            return base.GetPagedResult(entity, page, count);
        }

        public override IQueryable<BookEntity> GetAllWhere(Expression<Func<BookEntity, bool>> predicate)
        {
            var entities = _context.Set<BookEntity>().Where(predicate);
            return entities;
        }

        public override async Task<IEnumerable<BookEntity>> GetAllAsync()
        {
            var entities = await _context.Set<BookEntity>().ToListAsync();
            return entities;
        }
    }
}


