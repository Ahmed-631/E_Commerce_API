using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Domain.Contracts;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : Entity<TKey>;
    IRepository<TEntity, int> GetRepository<TEntity>() where TEntity : Entity<int>;

}
