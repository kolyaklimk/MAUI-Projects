using _153504_Klimkovich.Domain.Abstractions;
using _153504_Klimkovich.Domain.Entities;
using _153504_Klimkovich.Persistense.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _153504_Klimkovich.Persistense.Repository;

public class EfUnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Lazy<IRepository<SportTeam>> _sportTeamRepository;
    private readonly Lazy<IRepository<Member>> _memberRepository;

    public EfUnitOfWork(AppDbContext context)
    {
        _context = context;
        _sportTeamRepository = new Lazy<IRepository<SportTeam>>(() => new EfRepository<SportTeam>(context));
        _memberRepository = new Lazy<IRepository<Member>>(() => new EfRepository<Member>(context));
    }

    public IRepository<SportTeam> SportTeamRepository => _sportTeamRepository.Value;
    public IRepository<Member> MemberRepository => _memberRepository.Value;

    public async Task CreateDatabaseAsync()
    {
        await _context.Database.EnsureCreatedAsync();
    }

    public async Task RemoveDatbaseAsync()
    {
        await _context.Database.EnsureDeletedAsync();
    }

    public async Task SaveAllAsync()
    {
        await _context.SaveChangesAsync();
    }
}
