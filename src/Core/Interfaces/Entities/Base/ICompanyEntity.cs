namespace Core.Interfaces.Entities.Base;

public interface ICompanyEntity<TType>
    where TType : struct
{
    TType CompanyId { get; set; }
}

public interface ICompanyEntity : ICompanyEntity<Guid>
{

}
