using _153504_Klimkovich.Application.Service;
using _153504_Klimkovich.UI.ViewModels;


namespace _153504_Klimkovich.UI.Pages;

public partial class SportTeamsPage : ContentPage
{
    public SportTeamsPage(SportTeamsViewModel model)
	{
        InitializeComponent();
        BindingContext = model;
	}
}