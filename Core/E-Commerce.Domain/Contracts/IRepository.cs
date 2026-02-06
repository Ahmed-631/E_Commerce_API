using E_Commerce.Domain.Entities;

namespace E_Commerce.Domain.Contracts;

public interface IRepository<TEntity, TKey>
    where TEntity : Entity<TKey>
{
    void Add(TEntity entity);
    void Remove(TEntity entity);
    void Update(TEntity entity);
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
}
