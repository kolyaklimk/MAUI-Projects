using _153504_Klimkovich.Domain.Abstractions;
using _153504_Klimkovich.Domain.Entities;
using _153504_Klimkovich.Persistense.Data;

namespace _153504_Klimkovich.Persistense.Repository;

public class FakeUnitOfWork: IUnitOfWork
{
    private Lazy<IRepository<SportTeam>> _sportTeamRepository;
    private Lazy<IRepository<Member>> _memberRepository;

    public FakeUnitOfWork()
    {
        _sportTeamRepository = new Lazy<IRepository<SportTeam>>(() => new FakeSportTeamRepository());
        _memberRepository = new Lazy<IRepository<Member>>(() => new FakeMemberRepository());
    }

    public IRepository<SportTeam> SportTeamRepository => _sportTeamRepository.Value;
    public IRepository<Member> MemberRepository => _memberRepository.Value;

    public async Task CreateDatabaseAsync()
    {
        _sportTeamRepository = new();
        _memberRepository = new();
    }

    public async Task RemoveDatbaseAsync()
    {
        _memberRepository = null;
        _sportTeamRepository = null;
    }

    public async Task SaveAllAsync()
    {
    }
}
