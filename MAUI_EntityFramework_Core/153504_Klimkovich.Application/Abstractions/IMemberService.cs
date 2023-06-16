using _153504_Klimkovich.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _153504_Klimkovich.Application.Abstractions;

public interface IMemberService: IBaseService<Member>
{
    Task<IEnumerable<Member>> GetSportTeamMembers(int Id);
}
