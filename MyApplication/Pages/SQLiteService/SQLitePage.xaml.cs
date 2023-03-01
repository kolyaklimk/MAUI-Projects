using MyApplication.Pages.SQLiteService.Entities;
using MyApplication.Pages.SQLiteService.Services;
using System.Diagnostics; 
namespace MyApplication;

public partial class SQLitePage : ContentPage
{
    private readonly IDbService _db;
    public SQLitePage(IDbService db)
    {
        InitializeComponent();
        _db = db;
        _db.Init();
        picker.ItemsSource = _db.GetAllSportTeams().ToList();
    }
    private void picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<string> list = new List<string>();
        foreach(var i in _db.GetSportTeamMembers(picker.SelectedIndex))
        {
            list.Add(i.Name);
        }
        collection.ItemsSource = list;
    }
}