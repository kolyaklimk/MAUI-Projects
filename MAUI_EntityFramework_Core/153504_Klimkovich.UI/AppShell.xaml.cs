using _153504_Klimkovich.UI.Pages;

namespace _153504_Klimkovich.UI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MemberDetailsPage), typeof(MemberDetailsPage));
        Routing.RegisterRoute(nameof(CreateSportTeamPage), typeof(CreateSportTeamPage));
        Routing.RegisterRoute(nameof(CreateOrEditMemberPage), typeof(CreateOrEditMemberPage));
    }
}
