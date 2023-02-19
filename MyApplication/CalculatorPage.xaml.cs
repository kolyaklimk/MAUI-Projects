namespace MyApplication;

public partial class CalculatorPage : ContentPage
{
    public CalculatorPage()
    {
        InitializeComponent();
    }

    private double mainSolve = 0;
    private double memory = 0;
    private char operation = '0';
    private int maxSize = 10;

    private void EnableButtons()
    {
        button0.IsEnabled = true; button5.IsEnabled = true;
        button1.IsEnabled = true; button6.IsEnabled = true;
        button2.IsEnabled = true; button7.IsEnabled = true;
        button3.IsEnabled = true; button8.IsEnabled = true;
        button4.IsEnabled = true; button9.IsEnabled = true;
        buttonPlusAndMinus.IsEnabled = true; buttonPowHalf.IsEnabled = true;
        buttonPercent.IsEnabled = true; buttonMMinus.IsEnabled = true;
        buttonComma.IsEnabled = true; buttonSolve.IsEnabled = true;
        buttonMPlus.IsEnabled = true; buttonMinus.IsEnabled = true;
        buttonPow2.IsEnabled = true; buttonDivX.IsEnabled = true;
        buttonPow.IsEnabled = true; buttonPlus.IsEnabled = true;
        buttonMul.IsEnabled = true; buttonDiv.IsEnabled = true;
        buttonDel.IsEnabled = true; buttonCE.IsEnabled = true;
        buttonC.BackgroundColor = Color.FromRgba("#323232");
        EnableMemoryButtons();

        workSpace.Text = "0";
        secondSpace.Text = "";
        mainSolve = 0;
        memory = 0;
        operation = '0';
    }
    private void DisableButtons()
    {
        workSpace.Text = "Error";
        button0.IsEnabled = false; button5.IsEnabled = false;
        button1.IsEnabled = false; button6.IsEnabled = false;
        button2.IsEnabled = false; button7.IsEnabled = false;
        button3.IsEnabled = false; button8.IsEnabled = false;
        button4.IsEnabled = false; button9.IsEnabled = false;
        buttonPlusAndMinus.IsEnabled = false; buttonPowHalf.IsEnabled = false;
        buttonPercent.IsEnabled = false; buttonMMinus.IsEnabled = false;
        buttonComma.IsEnabled = false; buttonSolve.IsEnabled = false;
        buttonMPlus.IsEnabled = false; buttonMinus.IsEnabled = false;
        buttonPow2.IsEnabled = false; buttonDivX.IsEnabled = false;
        buttonPow.IsEnabled = false; buttonPlus.IsEnabled = false;
        buttonMul.IsEnabled = false; buttonDiv.IsEnabled = false;
        buttonDel.IsEnabled = false; buttonCE.IsEnabled = false;
        DisableMemoryButtons();
        buttonC.BackgroundColor = Colors.Gray;
    }
    private void DisableMemoryButtons()
    {
        buttonMC.IsEnabled = false; buttonMR.IsEnabled = false;
        buttonMC.TextColor = Colors.Gray; buttonMR.TextColor = Colors.Gray;
        memory = 0;
    }
    private void EnableMemoryButtons()
    {
        if (!buttonMC.IsEnabled)
        {
            buttonMC.IsEnabled = true; buttonMR.IsEnabled = true;
            buttonMC.TextColor = Colors.White; buttonMR.TextColor = Colors.White;
        }
    }
    private void PrintNum(double buf)
    {
        if (buf > 1e10 || ((buf < 1e-9) && buf > 0))
            workSpace.Text = string.Format("{0:0.0#####E0}", buf);
        else
            workSpace.Text = buf.ToString().Substring(0, Math.Min(maxSize, buf.ToString().Length));
        return;
    }

    private void DoOperation(double num, char op)
    {
        switch (operation)
        {
            case '+': mainSolve += num; break;

            case '-': mainSolve -= num; break;

            case '*': mainSolve *= num; break;

            case '/': mainSolve /= num; break;

            case '^': mainSolve = Math.Pow(mainSolve, num); break;

            default: mainSolve = num; break;
        }
        if (double.IsNegativeInfinity(mainSolve) ||
            double.IsNaN(mainSolve) ||
            double.IsPositiveInfinity(mainSolve)) { DisableButtons(); return; }
        if (double.IsNegativeInfinity(num) ||
            double.IsNaN(num) ||
            double.IsPositiveInfinity(num)) { DisableButtons(); return; }
        operation = op;
    }

    private void OnButtonClicked(object sender, System.EventArgs e)
    {
        Button button = (Button)sender;

        string buf1 = "";
        double buf2;

        switch (button.Text)
        {
            case "=":
                if (secondSpace.Text.Length != 0 && operation != '=')
                {
                    if (operation is '+' or '/' or '*' or '-' or '^')
                    {
                        DoOperation(Convert.ToDouble(workSpace.Text), '0');
                        if (!buttonCE.IsEnabled) return;
                        secondSpace.Text += workSpace.Text + '=';
                    }
                    else secondSpace.Text += '=';

                    PrintNum(mainSolve);
                    operation = '=';
                }
                return;

            case "CE":
                workSpace.Text = "0";
                operation = '0';
                return;

            case "C":
                if (!buttonCE.IsEnabled) { EnableButtons(); }
                secondSpace.Text = "";
                workSpace.Text = "0";
                operation = '0';
                return;

            case "del":
                if (workSpace.Text.IndexOf('E') == -1)
                {
                    if (workSpace.Text.Length > 1 && workSpace.Text.IndexOf('E') == -1)
                    {
                        workSpace.Text = workSpace.Text.Remove(workSpace.Text.Length - 1, 1);
                    }
                    else workSpace.Text = "0";
                }
                return;

            case "1/x":
                buf2 = (1 / Convert.ToDouble(workSpace.Text));
                if (operation is '+' or '/' or '*' or '-' or '^')
                    secondSpace.Text += "1/" + workSpace.Text;
                else
                    secondSpace.Text = "1/" + workSpace.Text;
                PrintNum(buf2);
                DoOperation(buf2, '0');
                return;

            case "x^2":
                buf2 = Math.Pow(Convert.ToDouble(workSpace.Text), 2);
                if (operation is '+' or '/' or '*' or '-' or '^')
                    secondSpace.Text += workSpace.Text + "^2";
                else
                    secondSpace.Text = workSpace.Text + "^2";
                PrintNum(buf2);
                DoOperation(buf2, '0');
                return;

            case "x^1/2":
                buf2 = (Math.Sqrt(Convert.ToDouble(workSpace.Text)));

                if (operation is '+' or '/' or '*' or '-' or '^')
                    secondSpace.Text += workSpace.Text + "^1/2";
                else
                    secondSpace.Text = workSpace.Text + "^1/2";
                PrintNum(buf2);
                DoOperation(buf2, '0');
                return;

            case "%":
                buf2 = (Convert.ToDouble(workSpace.Text) * 0.01);

                if (operation is '+' or '/' or '*' or '-' or '^')
                    secondSpace.Text += workSpace.Text + "%";
                else
                    secondSpace.Text = workSpace.Text + "%";
                PrintNum(buf2);
                DoOperation(buf2, '0');
                return;

            case ",":
                if (workSpace.Text.Length < maxSize - 1 && workSpace.Text.IndexOf(',') == -1)
                {
                    workSpace.Text += ",";
                }
                return;

            case "+/-":
                if (workSpace.Text[0] == '0') return;
                if (workSpace.Text[0] != '-') workSpace.Text = '-' + workSpace.Text;
                else workSpace.Text = workSpace.Text.Remove(0, 1);
                return;

            case "x^y":
                DoOperation(Convert.ToDouble(workSpace.Text), '^');
                if (!buttonCE.IsEnabled) return;
                if (mainSolve > 1e10)
                    secondSpace.Text = string.Format("{0:0.0##E0}", mainSolve) + '^';
                else
                {
                    buf1 = Convert.ToString(mainSolve);
                    secondSpace.Text = buf1.Substring(0, Math.Min(maxSize, buf1.Length)) + '^';
                }
                workSpace.Text = "0";
                return;

            case "M+":
                EnableMemoryButtons();
                memory += Convert.ToDouble(workSpace.Text);
                if (double.IsNegativeInfinity(memory) ||
                    double.IsNaN(memory) ||
                    double.IsPositiveInfinity(memory)) { DisableButtons(); return; }
                return;

            case "M-":
                EnableMemoryButtons();
                memory -= Convert.ToDouble(workSpace.Text);
                if (double.IsNegativeInfinity(memory) ||
                    double.IsNaN(memory) ||
                    double.IsPositiveInfinity(memory)) { DisableButtons(); return; }
                return;

            case "MC":
                DisableMemoryButtons();
                return;

            case "MR":
                PrintNum(memory);
                return;
        }

        if (button.Text[0] <= '9' && button.Text[0] >= '0')
        {
            if (operation == '=') secondSpace.Text = "";
            if (workSpace.Text == "0") workSpace.Text = button.Text;
            else
            {
                if (workSpace.Text[0] == '-')
                {
                    if (workSpace.Text.Length <= maxSize) workSpace.Text += button.Text;
                }
                else if (workSpace.Text.Length < maxSize) workSpace.Text += button.Text;
            }
            return;
        }

        if (button.Text[0] is '+' or '/' or '*' or '-')
        {
            DoOperation(Convert.ToDouble(workSpace.Text), button.Text[0]);
            if (!buttonCE.IsEnabled) return;
            if (mainSolve > 1e10)
                secondSpace.Text = string.Format("{0:0.0##E0}", mainSolve) + button.Text[0];
            else
            {
                buf1 = Convert.ToString(mainSolve);
                secondSpace.Text = buf1.Substring(0, Math.Min(maxSize, buf1.Length)) + button.Text[0];
            }
            workSpace.Text = "0";
            return;
        }
    }
}