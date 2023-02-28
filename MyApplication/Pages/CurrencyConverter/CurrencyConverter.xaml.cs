using MyApplication.Pages.CurrencyConverter.Services;

namespace MyApplication.Pages.CurrencyConverter;

public partial class CurrencyConverter : ContentPage
{
    private readonly IRateService _rateService;	
	public CurrencyConverter(IRateService rate)
	{
		InitializeComponent();
		_rateService = rate;
	}
}