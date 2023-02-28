using MyApplication.Pages.CurrencyConverter.Services;
using NbrbAPI.Models;

namespace MyApplication;

public partial class CurrencyConverterPage : ContentPage
{
    private readonly IRateService _rateService;
	private List<Rate> rate;
	public CurrencyConverterPage(IRateService rate)
	{
		InitializeComponent();
		_rateService = rate;
	}

    private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {

    }
}