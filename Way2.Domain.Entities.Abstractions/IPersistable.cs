namespace Way2.Domain.Entities.Abstractions
{

    public interface IPersistable
    {
        object GetId();
        
        bool IsNew();

        long Version { get; set; }
    }

    public interface IPersistable<TKey> : IPersistable
    {
        TKey Id { get; set; }
    }

}
