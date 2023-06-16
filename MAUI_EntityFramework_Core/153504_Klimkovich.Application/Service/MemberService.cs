using _153504_Klimkovich.Application.Abstractions;
using _153504_Klimkovich.Domain.Abstractions;
using _153504_Klimkovich.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _153504_Klimkovich.Application.Service;

public class MemberService: IMemberService
{
    private IUnitOfWork _unitOfWork;

    public MemberService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;    
    }
    public async Task<IEnumerable<Member>> GetSportTeamMembers(int Id)
    {
        return await _unitOfWork.MemberRepository.ListAsync(x => x.SportTeam.Id == Id);
    }
    public async Task AddAsync(Member item)
    {
        await _unitOfWork.MemberRepository.AddAsync(item);
        await _unitOfWork.SaveAllAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var result = await _unitOfWork.MemberRepository.GetByIdAsync(id);

        if(result != null)
        {
            await _unitOfWork.MemberRepository.DeleteAsync(result);
        }
        await _unitOfWork.SaveAllAsync();
    }

    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        return await _unitOfWork.MemberRepository.ListAllAsync();
    }

    public async Task<Member> GetByIdAsync(int id)
    {
        return await _unitOfWork.MemberRepository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(Member item)
    {
        await _unitOfWork.MemberRepository.UpdateAsync(item);
        await _unitOfWork.SaveAllAsync();
    }
}
