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

[QueryProperty(nameof(Member), nameof(Member))]
[QueryProperty(nameof(SportTeams), nameof(SportTeams))]
public partial class MemberDetailsViewModel : ObservableObject
{
    private IMemberService _memberService;
    public MemberDetailsViewModel(IMemberService memberService)
    {
        _memberService = memberService;
    }
    public ObservableCollection<SportTeam> SportTeams { get; set; } = new();

    [ObservableProperty]
    SportTeam selectedSportTeam;

    [ObservableProperty]
    private Member member;

    [RelayCommand]
    async void EditMember() => await GotoEditMemberPage();

    [RelayCommand]
    async void ChangeSportTeam() => await Change();

    [RelayCommand]
    void LoadSportTeams() => OnPropertyChanged(nameof(SportTeams));

    [RelayCommand]
    async void ChooseImage() => await ChooseImageMember();

    private async Task ChooseImageMember()
    {
        string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MyApp\\Images\\";
        Directory.CreateDirectory(dir);
        var filePath = await FilePicker.PickAsync(new PickOptions { FileTypes = FilePickerFileType.Png, });

        if (filePath != null)
        {
            File.Copy(filePath.FullPath, Path.Combine(dir, Member.Id.ToString() + ".png"),true);
            OnPropertyChanged(nameof(Member));
            MessagingCenter.Send(this, "UpdateMembers");
        }
    }

    private async Task GotoEditMemberPage()
    {
        await Shell.Current.GoToAsync(nameof(CreateOrEditMemberPage),
            new Dictionary<string, object>() {
            { nameof(SportTeam), Member.SportTeam },
            { nameof(Member), Member }});
    }
    private async Task Change()
    {
        Member.SportTeam = SelectedSportTeam;
        await _memberService.UpdateAsync(Member);

        MessagingCenter.Send(this, "UpdateMembers");
    }
}
