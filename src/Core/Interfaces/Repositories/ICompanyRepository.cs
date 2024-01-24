using Core.Entities;
using Core.Interfaces.Repositories.Base; 

namespace Core.Interfaces.Repositories;

public interface ICompanyRepository : IBaseRepository<Company>
{
    Task<Company?> GetCompanyWithEmployeesAsync(Guid id);
    Task<bool> IsCompanyActiveAsync(Guid companyId);
}
