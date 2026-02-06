
namespace ECommerce.Persistance.Repositories;

public class UnitOfWork(StoreDbContext dbContext)
    : IUnitOfWork
{
    private readonly Dictionary<string, object> _repositories = [];
    public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : Entity<TKey>
    {
        var typeName = typeof(TEntity).Name;
        if (_repositories.TryGetValue(typeName, out object? value))
            return (value as IRepository<TEntity, TKey>)!;
         
        var repo = new Repository<TEntity, TKey>(dbContext);
        _repositories.Add(typeName, repo);
        return repo;
    }

    public IRepository<TEntity, int> GetRepository<TEntity>() where TEntity : Entity<int> 
        => GetRepository<TEntity, int>(); 

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await dbContext.SaveChangesAsync(cancellationToken);
}
