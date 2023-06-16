using _153504_Klimkovich.UI.ViewModels;

namespace _153504_Klimkovich.UI.Pages;

public partial class CreateSportTeamPage : ContentPage
{
    public CreateSportTeamPage(CreateSportTeamViewModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (enty1.Text != null && enty2.Text != null)
            await Navigation.PopAsync();
    }
}