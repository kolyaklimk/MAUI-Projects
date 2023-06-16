using _153504_Klimkovich.Application.Abstractions;
using _153504_Klimkovich.Application.Service;
using _153504_Klimkovich.Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace _153504_Klimkovich.UI.ViewModels;

[QueryProperty(nameof(Member), nameof(Member))]
[QueryProperty(nameof(SportTeam), nameof(SportTeam))]
public partial class CreateOrEditMemberViewModel: ObservableObject
{
    private readonly IMemberService _memberService;
    public CreateOrEditMemberViewModel(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [ObservableProperty]
    private SportTeam sportTeam;

    [ObservableProperty]
    private Member member;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private int age;

    [ObservableProperty]
    private int points;


    [RelayCommand]
    async void CreateOrEdit() => await CreateOrEditMember();

    [RelayCommand]
    async void LoadToEdit() => LoadToEditMember();

    [RelayCommand]
    async void ChooseImage() => await ChooseImageMember();

    private async Task ChooseImageMember()
    {
        string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MyApp\\Images\\";
        Directory.CreateDirectory(dir);
        var filePath = await FilePicker.PickAsync(new PickOptions { FileTypes = FilePickerFileType.Png, });

        if(filePath != null && Member != null)
        {
            File.Copy(filePath.FullPath,
                Path.Combine(dir, Member.Id.ToString() + ".png"), true);
            OnPropertyChanged(nameof(Member));
            MessagingCenter.Send(this, "UpdateMembers");
        }
    }
    private async Task CreateOrEditMember()
    {
        if(Name != null && Age != null && Points != null)
        {
            if(Member == null)
            {
                await _memberService.AddAsync(new Member()
                {
                    Age = Age,
                    Name = Name,
                    SportTeam = SportTeam,
                    Points = Points
                });
            }
            else
            {
                Member.Age = Age;
                Member.Name = Name;
                Member.Points = Points;
            }
            MessagingCenter.Send(this, "UpdateMembers");
        }
    }

    private void LoadToEditMember()
    {
        if(Member != null)
        {
            Age = Member.Age;
            Name = Member.Name;
            Points = Member.Points;
        }
    }
}
