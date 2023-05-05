using GeekBurguer_Promotion_Model.Promotion;

namespace GeekBurguer_Promotion_Infrastructure.Repository.Default.Interface
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : IAggregateRoot
    {
        Task Add(TEntity entity, CancellationToken cancellationToken = default);
        Task AddRange(List<TEntity> entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        void UpdateRange(HashSet<TEntity> entity, CancellationToken cancellationToken = default);
        Task<List<T>> GetAllTObject<T>() where T : Base;
        void Update<T>(T entity, params string[] changedPropertyNames) where T : class;


    }
}
