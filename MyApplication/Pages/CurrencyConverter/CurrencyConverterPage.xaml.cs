using MyApplication.Pages.CurrencyConverter.Services;
using NbrbAPI.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MyApplication;

public partial class CurrencyConverterPage : ContentPage
{
    private readonly IRateService _rateService;
    private List<Rate> rate = new();
    private Rate buf;
    private bool mainConverter = true;
    public CurrencyConverterPage(IRateService rate)
    {
        InitializeComponent();
        _rateService = rate;
        calendar.MaximumDate=DateTime.Today;
    }

    private async void calendar_DateSelected(object sender, DateChangedEventArgs e)
    {
        rate = (await _rateService.GetRates(calendar.Date)).ToList();
        pickerCurrency.ItemsSource = rate;
    }

    private void pickerCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        buf = pickerCurrency.SelectedItem as Rate;
        if (buf != null)
        {
            secondEntry.Text = "";
            firstEntry.Text = "";
            firstLabel.Text = (pickerCurrency.SelectedItem as Rate).Cur_Abbreviation;
            secondLabel.Text = "BYN";
            mainConverter = true;
        }
    }
    private void firstEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (buf != null)
        {
            if (mainConverter)
            {
                try
                {
                    secondEntry.Text = (Math.Round(Convert.ToDouble(buf.Cur_OfficialRate.ToString()) *
                    Convert.ToDouble(firstEntry.Text), 4)).ToString();
                }
                catch { }
            }
            else
            {
                try
                {
                    secondEntry.Text = (Math.Round(Convert.ToDouble(firstEntry.Text) /
                        Convert.ToDouble(buf.Cur_OfficialRate.ToString()), 4)).ToString();
                }
                catch { }
            }
        }
    }

    private void changeMainConverter_Clicked(object sender, EventArgs e)
    {
        if (buf != null)
        {
            if (mainConverter)
            {
                secondLabel.Text = (pickerCurrency.SelectedItem as Rate).Cur_Abbreviation;
                firstLabel.Text = "BYN";
                secondEntry.Text = "";
                firstEntry.Text = "";
                mainConverter = !mainConverter;
            }
            else
            {
                firstLabel.Text = (pickerCurrency.SelectedItem as Rate).Cur_Abbreviation;
                secondLabel.Text = "BYN";
                secondEntry.Text = "";
                firstEntry.Text = "";
                mainConverter = !mainConverter;
            }
        }
    }
}