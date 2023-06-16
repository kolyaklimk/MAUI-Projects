using _153504_Klimkovich.Application.Abstractions;
using _153504_Klimkovich.Domain.Abstractions;
using _153504_Klimkovich.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _153504_Klimkovich.Application.Service;

public class SportTeamService: ISportTeamService
{
    private IUnitOfWork _unitOfWork;
    public SportTeamService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task AddAsync(SportTeam item)
    {
        await _unitOfWork.SportTeamRepository.AddAsync(item);
        await _unitOfWork.SaveAllAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var result = await _unitOfWork.SportTeamRepository.GetByIdAsync(id);

        if(result != null)
        {
            await _unitOfWork.SportTeamRepository.DeleteAsync(result);
        }
        await _unitOfWork.SaveAllAsync();
    }

    public async Task<IEnumerable<SportTeam>> GetAllAsync()
    {
        return await _unitOfWork.SportTeamRepository.ListAllAsync();
    }

    public async Task<SportTeam> GetByIdAsync(int id)
    {
        return await _unitOfWork.SportTeamRepository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(SportTeam item)
    {
        await _unitOfWork.SportTeamRepository.UpdateAsync(item);
        await _unitOfWork.SaveAllAsync();
    }
}
