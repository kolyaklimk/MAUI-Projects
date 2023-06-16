using _153504_Klimkovich.Application.Abstractions;
using _153504_Klimkovich.Domain.Entities;
using _153504_Klimkovich.UI.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _153504_Klimkovich.UI.ViewModels;

public partial class SportTeamsViewModel : ObservableObject
{
    private readonly ISportTeamService _sportTeamService;
    private readonly IMemberService _memberService;
    public SportTeamsViewModel(ISportTeamService sportTeamService, IMemberService memberService)
    {
        _sportTeamService = sportTeamService;
        _memberService = memberService;
        SubscribeMessages();
    }
    public ObservableCollection<SportTeam> SportTeams { get; set; } = new();
    public ObservableCollection<Member> Members { get; set; } = new();


    [ObservableProperty]
    SportTeam selectedSportTeam;

    [ObservableProperty]
    bool isVisible;

    [RelayCommand]
    async void UpdateSportTeamList() => await GetSportTeams();

    [RelayCommand]
    async void UpdateMembersList() => await GetMembers();

    [RelayCommand]
    async void ShowDetails(Member member) => await GotoDetailsPage(member);

    [RelayCommand]
    async void CreateSportTeam() => await GotoCreateSportTeamPage();

    [RelayCommand]
    async void CreateMember() => await GotoCreateMemberPage();


    private async Task GotoDetailsPage(Member member)
    {
        await Shell.Current.GoToAsync(nameof(MemberDetailsPage),
            new Dictionary<string, object>() {
                { nameof(Member), member },
                { "SportTeams", SportTeams }});
    }

    private async Task GotoCreateSportTeamPage()
    {
        await Shell.Current.GoToAsync(nameof(CreateSportTeamPage));
    }

    private async Task GotoCreateMemberPage()
    {
        if (SelectedSportTeam != null)
            await Shell.Current.GoToAsync(nameof(CreateOrEditMemberPage),
                new Dictionary<string, object>() { { nameof(SportTeam), SelectedSportTeam } });
    }

    public async Task GetSportTeams()
    {
        var sportTeams = await _sportTeamService.GetAllAsync();
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            SportTeams.Clear();
            foreach (var sportTeam in sportTeams)
                SportTeams.Add(sportTeam);
        });
    }
    public async Task GetMembers()
    {
        if (SelectedSportTeam == null)
        {
            Members.Clear();
            IsVisible = false;
            return;
        }
        var members = await _memberService.GetSportTeamMembers(SelectedSportTeam.Id);
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            Members.Clear();
            foreach (var member in members)
            {
                Members.Add(member);
            }
        });
        IsVisible = true;
    }
    private void SubscribeMessages()
    {
        MessagingCenter.Subscribe<CreateSportTeamViewModel>(this,
            "UpdateSportTeams", async (sender) => await GetSportTeams());

        MessagingCenter.Subscribe<CreateOrEditMemberViewModel>(this,
            "UpdateMembers", async (sender) => await GetMembers());

        MessagingCenter.Subscribe<MemberDetailsViewModel>(this,
            "UpdateMembers", async (sender) => await GetMembers());
    }
}
