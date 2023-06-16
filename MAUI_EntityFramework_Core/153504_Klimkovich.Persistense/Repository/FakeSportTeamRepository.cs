using _153504_Klimkovich.Domain.Abstractions;
using _153504_Klimkovich.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace _153504_Klimkovich.Persistense.Repository;

public class FakeSportTeamRepository: IRepository<SportTeam>
{
    List<SportTeam> _sportTeams;
    public FakeSportTeamRepository()
    {
        _sportTeams = new List<SportTeam>()
        { new SportTeam(){ Id=1, Name="Nik", KindOfSport="Basketball", Members=new List<Member>()},
        { new SportTeam(){ Id=2, Name="Klim", KindOfSport="Football", Members=new List<Member>()}} };
    }
    public async Task<IReadOnlyList<SportTeam>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => _sportTeams);
    }
    public async Task<SportTeam> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<SportTeam, object>>[]? includesProperties)
    {
        throw new NotImplementedException();
    }
    public Task AddAsync(SportTeam entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(SportTeam entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<SportTeam> FirstOrDefaultAsync(Expression<Func<SportTeam, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<SportTeam>> ListAsync(Expression<Func<SportTeam, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<SportTeam, object>>[]? includesProperties)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(SportTeam entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
