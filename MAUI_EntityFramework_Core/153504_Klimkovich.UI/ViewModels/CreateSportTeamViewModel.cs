using _153504_Klimkovich.Application.Abstractions;
using _153504_Klimkovich.Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _153504_Klimkovich.UI.ViewModels;

public partial class CreateSportTeamViewModel: ObservableObject
{
    private readonly ISportTeamService _sportTeamService;
    public CreateSportTeamViewModel(ISportTeamService sportTeamService)
    {
        _sportTeamService = sportTeamService;
    }

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string kindOfSport;


    [RelayCommand]
    async void Create() => await CreateSportTeam();

    private async Task CreateSportTeam()
    {
        if (Name != null && KindOfSport != null)
        {
            await _sportTeamService.AddAsync(new SportTeam() { 
                KindOfSport = KindOfSport, Name = Name });
            MessagingCenter.Send(this, "UpdateSportTeams");
        }
    }
}
