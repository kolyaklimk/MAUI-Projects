using _153504_Klimkovich.Domain.Entities;
using _153504_Klimkovich.UI.ViewModels;

namespace _153504_Klimkovich.UI.Pages;

public partial class MemberDetailsPage : ContentPage
{
	public MemberDetailsPage(MemberDetailsViewModel model)
	{
		InitializeComponent();
		BindingContext= model;
	}
}