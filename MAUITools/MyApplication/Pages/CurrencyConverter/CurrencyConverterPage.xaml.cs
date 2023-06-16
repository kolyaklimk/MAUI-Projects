using MyApplication.Pages.CurrencyConverter.Services;
using NbrbAPI.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MyApplication;

public partial class CurrencyConverterPage : ContentPage
{
    private readonly IRateService _rateService;
    private Rate buf;
    private bool mainConverter = true;
    public CurrencyConverterPage(IRateService rate)
    {
        InitializeComponent();
        _rateService = rate;
        calendar.MaximumDate = DateTime.Today;
    }

    private async void calendar_DateSelected(object sender, DateChangedEventArgs e)
    {
        loadLabel.Text = "Load";
        var picker = (await _rateService.GetRates(calendar.Date));
        if (picker == null)
        {
            loadLabel.Text = "Error! No internet connection!";
        }
        else
        {
            pickerCurrency.ItemsSource = picker.ToList();
            loadLabel.Text = "";
        }
        firstEntry.Text = "";
        secondEntry.Text = "";
        firstLabel.Text = "";
        secondLabel.Text = "";
        firstEntry.IsReadOnly = true;
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
            firstEntry.IsReadOnly = false;
        }
    }
    private void firstEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (buf != null)
        {
            try
            {
                if (mainConverter)
                    secondEntry.Text = (Math.Round(Convert.ToDouble(buf.Cur_OfficialRate.ToString()) *
                    Convert.ToDouble(firstEntry.Text), 4)).ToString();
                else
                    secondEntry.Text = (Math.Round(Convert.ToDouble(firstEntry.Text) /
                    Convert.ToDouble(buf.Cur_OfficialRate.ToString()), 4)).ToString();
            }
            catch
            {
                secondEntry.Text = "Error";
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
            }
            else
            {
                firstLabel.Text = (pickerCurrency.SelectedItem as Rate).Cur_Abbreviation;
                secondLabel.Text = "BYN";
            }
            secondEntry.Text = "";
            firstEntry.Text = "";
            mainConverter = !mainConverter;
        }
    }
}