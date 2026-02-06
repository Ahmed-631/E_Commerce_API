namespace E_Commerce.Domain.Entities;

public abstract class Entity<TKey>
{
    public TKey Id { get; set; } = default!;
}
