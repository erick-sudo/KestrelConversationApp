namespace Core.Interfaces.Entities.Base;

public interface IBaseEntity<TType> where TType : struct
{
    TType Id { get; set; }
}

public interface IBaseEntity : IBaseEntity<Guid>
{

}
