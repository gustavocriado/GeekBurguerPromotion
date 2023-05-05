using GeekBurguer_Promotion_Infrastructure.Context;
using GeekBurguer_Promotion_Infrastructure.Repository.Default.Interface;
using GeekBurguer_Promotion_Model.Promotion;
using Microsoft.EntityFrameworkCore;

namespace GeekBurguer_Promotion_Infrastructure.Repository.Default
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IAggregateRoot
    {
        protected GeekBurguerContext _db;

        public BaseRepository(GeekBurguerContext db)
        {
            _db = db;
        }

        public async Task Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _db.AddAsync(entity, cancellationToken);
            await _db.SaveChangesAsync();
        }

        public async Task AddRange(List<TEntity> entity, CancellationToken cancellationToken = default) => await _db.AddRangeAsync(entity, cancellationToken);

        public void Update(TEntity entity)
        {
            _db.Update(entity);
            _db.SaveChanges();
        }

        public void Update<T>(T entity, params string[] changedPropertyNames) where T : class
        {
            _db.Update(entity);
            _db.SaveChanges();
        }
        public void UpdateRange(HashSet<TEntity> entity, CancellationToken cancellationToken = default) => _db.UpdateRange(entity, cancellationToken);


        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<List<T>> GetAllTObject<T>() where T : Base
        {
            return await _db.Set<T>().ToListAsync();
        }

        public void Delete<T>(T entity) where T : Base
        {
            _db.Set<T>().Remove(entity);
        }
    }
}
