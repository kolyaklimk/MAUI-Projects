using MyApplication.Services;
using MyApplication.Entities;
namespace MyApplication;

public partial class SQLitePage : ContentPage
{
    private readonly IDbService _bService;
    public SQLitePage()
    {
        InitializeComponent();
        _bService.Init();
        foreach (var a in _bService.GetAllSportTeams())
        {
            picker.Items.Add(a.Name);
        }
    }
}