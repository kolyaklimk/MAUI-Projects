using MyApplication.Entities;
using MyApplication.Pages.SQLite.Services;

namespace MyApplication;

public partial class SQLitePage : ContentPage
{
    private readonly IDbService _db;
    public SQLitePage(IDbService db)
    {
        InitializeComponent();
        _db = db;
        _db.Init();
        foreach(var i in _db.GetAllSportTeams()) {
            picker.Items.Add(i.Name);
        }
    }

    private void picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        collection.ItemsSource=_db.GetSportTeamMembers()
    }
}