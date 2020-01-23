namespace Way2.Domain.Entities.Abstractions
{

    public interface IEntity<TKey> : IAuditable, IPersistable<TKey>
    {
    }

}
