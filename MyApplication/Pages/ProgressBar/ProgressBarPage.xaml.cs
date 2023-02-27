using System;
using System.Collections.Generic;

namespace MyApplication;

public partial class ProgressBarPage : ContentPage
{
    CancellationTokenSource cancelTokenSource = new();
    public ProgressBarPage()
    {
        InitializeComponent();
    }
    private void printProgress(List<double> list)
    {
        switch (list[2])
        {
            case 0:
                myProgressBar.Progress = list[1];
                precent.Text = Convert.ToInt16(list[1] * 100).ToString() + '%';
                break;
            case 1:
                label.Text = Convert.ToString(list[0]);
                myProgressBar.Progress = 1;
                precent.Text = "100%";
                buttonStart.IsEnabled = true;
                buttonStop.IsEnabled = false;
                break;
            case 2:
                label.Text = "Canceled";
                myProgressBar.Progress = 0;
                precent.Text = "0%";
                buttonStart.IsEnabled = true;
                buttonStop.IsEnabled = false;
                break;
        }
    }
    private void CentralRectangle(double a, double b, double step,
        IProgress<List<double>> progress, CancellationToken token)
    {
        List<double> list = new List<double>() { 0, -1, 0 };
        double sum = (Math.Sin(a) + Math.Sin(b)) / 2;
        for (double i = 1; i < 1 / step; i++)
        {
            if (token.IsCancellationRequested)
            {
                list[2] = 2;
                progress?.Report(list);
                return;
            }
            double x = a + i * step;
            sum += Math.Sin(x);
            if (list[1] != Math.Round(i * step, 2))
            {
                list[1] = Math.Round(i * step, 2);
                progress?.Report(list);
            }
        }
        list[0] = sum * step;
        list[2] = 1;
        progress?.Report(list);
    }
    private async void Start(object sender, System.EventArgs e)
    {
        IProgress<List<double>> progress = new Progress<List<double>>(list => printProgress(list));
        CancellationToken token = cancelTokenSource.Token;
        label.Text = "Calculating";
        buttonStart.IsEnabled = false;
        buttonStop.IsEnabled = true;
        await Task.Run(() => CentralRectangle(0, 1, 0.0000001, progress, token));
    }
    private void Stop(object sender, System.EventArgs e)
    {
        cancelTokenSource.Cancel();
        cancelTokenSource.Dispose();
        cancelTokenSource = new();
        buttonStart.IsEnabled = true;
        buttonStop.IsEnabled = false;
    }
}