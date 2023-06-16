using _153504_Klimkovich.Domain.Abstractions;
using _153504_Klimkovich.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _153504_Klimkovich.Persistense.Repository;

public class FakeMemberRepository : IRepository<Member>
{
    List<Member> _list = new List<Member>();
    public FakeMemberRepository()
    {
        Random rand = new Random();
        int k = 1;
        for (int i = 1; i <= 2; i++)
            for (int j = 0; j < 10; j++)
                _list.Add(new Member()
                {
                    Id = k,
                    Name = $"Member {k++}",
                    SportTeam = new SportTeam() { Id = i },
                    Age = j,
                    Points = j * i
                });
    }

    public async Task<IReadOnlyList<Member>> ListAsync(Expression<Func<Member, bool>> filter,
        CancellationToken cancellationToken = default,
        params Expression<Func<Member, object>>[]? includesProperties)
    {
        var data = _list.AsQueryable();
        return data.Where(filter).ToList();
    }
    public Task AddAsync(Member entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Member entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Member> FirstOrDefaultAsync(Expression<Func<Member, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Member> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Member, object>>[]? includesProperties)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Member>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Member entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
