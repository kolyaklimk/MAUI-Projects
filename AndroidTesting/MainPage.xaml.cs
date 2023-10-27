namespace AndroidTesting
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            if (CounterBtn.Text != "Обработка")
            {
                ResultLabel.Text = "";
                CounterBtn.Text = "Обработка";
                string result = await Task.Run(() => Testing.Start(long.Parse(NumberEntry.Text), long.Parse(NumberEntry2.Text)));
                CounterBtn.Text = "Запустить тест";
                ResultLabel.Text = result;
            }
        }
    }
}