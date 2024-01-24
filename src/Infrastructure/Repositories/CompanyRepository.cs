using Core.Entities;
using Core.Interfaces.Repositories; 
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext context) : base(context)
    {

    }

    public async Task<Company?> GetCompanyWithEmployeesAsync(Guid id)
    {
        var company = await _dataContext.Companies
                                            .Where(x => x.Id == id)
                                            .Include(x => x.Employees)
                                            .FirstOrDefaultAsync();
        return company;
    }

    public async Task<bool> IsCompanyActiveAsync(Guid companyId)
    {
        return await _dataContext.Companies
            .AnyAsync(c => c.Id == companyId && c.IsActive);
    }
}
