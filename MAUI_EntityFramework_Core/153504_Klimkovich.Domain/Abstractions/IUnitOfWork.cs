using _153504_Klimkovich.Domain.Entities;

namespace _153504_Klimkovich.Domain.Abstractions;

public interface IUnitOfWork
{
    IRepository<SportTeam> SportTeamRepository { get; }
    IRepository<Member> MemberRepository { get; }
    public Task RemoveDatbaseAsync();
    public Task CreateDatabaseAsync();
    public Task SaveAllAsync();
}
