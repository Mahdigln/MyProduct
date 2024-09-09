using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Abstractions;
public interface IEntity
{
}

public interface IEntity<TKey> : IEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    TKey Id { get; set; }
}

public abstract class BaseEntity<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>
{
}
