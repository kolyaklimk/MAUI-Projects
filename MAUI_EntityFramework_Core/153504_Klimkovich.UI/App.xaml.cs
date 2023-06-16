
namespace _153504_Klimkovich.UI;

public partial class App : IApplication
{
    public App()
	{
		InitializeComponent();
        MainPage = new AppShell();
	}

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        const int newWidth = 400;
        const int newHeight = 700;

        window.Width = newWidth;
        window.Height = newHeight;

        return window;
    }
}
