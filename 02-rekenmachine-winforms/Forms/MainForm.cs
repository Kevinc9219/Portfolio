using RekenmachineWinForms.Models;

namespace RekenmachineWinForms.Forms;

public partial class MainForm : Form
{
    private readonly Calculator _calculator = new();
    private string _currentInput = "0";
    private bool _newInput = true;
    private bool _justCalculated = false;

    public MainForm()
    {
        InitializeComponent();
        WireUpEvents();
    }

    private void WireUpEvents()
    {
        btn0.Click += (_, _) => AppendDigit("0");
        btn1.Click += (_, _) => AppendDigit("1");
        btn2.Click += (_, _) => AppendDigit("2");
        btn3.Click += (_, _) => AppendDigit("3");
        btn4.Click += (_, _) => AppendDigit("4");
        btn5.Click += (_, _) => AppendDigit("5");
        btn6.Click += (_, _) => AppendDigit("6");
        btn7.Click += (_, _) => AppendDigit("7");
        btn8.Click += (_, _) => AppendDigit("8");
        btn9.Click += (_, _) => AppendDigit("9");
        btnDecimal.Click += (_, _) => AppendDecimal();

        btnAdd.Click      += (_, _) => SetOperator('+');
        btnSubtract.Click += (_, _) => SetOperator('-');
        btnMultiply.Click += (_, _) => SetOperator('*');
        btnDivide.Click   += (_, _) => SetOperator('/');

        btnEquals.Click    += (_, _) => Calculate();
        btnClear.Click     += (_, _) => Clear();
        btnBackspace.Click += (_, _) => Backspace();
        btnToggleSign.Click += (_, _) => ToggleSign();
        btnPercent.Click   += (_, _) => ApplyPercent();

        this.KeyDown += MainForm_KeyDown;
    }

    private void AppendDigit(string digit)
    {
        if (_justCalculated)
        {
            _calculator.Clear();
            _justCalculated = false;
            _newInput = true;
            lblExpression.Text = "";
        }

        if (_newInput)
        {
            _currentInput = digit;
            _newInput = false;
        }
        else
        {
            if (_currentInput == "0" && digit != "0")
                _currentInput = digit;
            else if (_currentInput != "0")
                _currentInput += digit;
        }

        UpdateDisplay();
    }

    private void AppendDecimal()
    {
        if (_justCalculated)
        {
            _calculator.Clear();
            _justCalculated = false;
            _newInput = true;
            lblExpression.Text = "";
        }

        if (_newInput)
        {
            _currentInput = "0,";
            _newInput = false;
        }
        else if (!_currentInput.Contains(','))
        {
            _currentInput += ",";
        }

        UpdateDisplay();
    }

    private void SetOperator(char op)
    {
        if (!_justCalculated)
        {
            // Chain: commit current input as first operand
            if (double.TryParse(_currentInput.Replace(',', '.'),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out double val))
            {
                _calculator.SetOperand(val);
            }
        }

        _calculator.SetOperator(op);
        _newInput = true;
        _justCalculated = false;
        lblExpression.Text = $"{Calculator.FormatNumber(_calculator.FirstOperand)} {DisplayOp(op)}";
    }

    private void Calculate()
    {
        if (!_calculator.HasOperator) return;

        if (!double.TryParse(_currentInput.Replace(',', '.'),
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture,
            out double current))
            return;

        try
        {
            var (result, expression) = _calculator.Calculate(current);
            AddToHistory($"{expression} {Calculator.FormatNumber(result)}");
            lblExpression.Text = expression;
            _currentInput = FormatResult(result);
            _justCalculated = true;
            _newInput = true;
            _calculator.Clear();
        }
        catch (DivideByZeroException)
        {
            lblDisplay.Text = "Fout: deling door 0";
            lblExpression.Text = "";
            _calculator.Clear();
            _currentInput = "0";
            _newInput = true;
            _justCalculated = false;
            return;
        }

        UpdateDisplay();
    }

    private void Clear()
    {
        _calculator.Clear();
        _currentInput = "0";
        _newInput = true;
        _justCalculated = false;
        lblExpression.Text = "";
        UpdateDisplay();
    }

    private void Backspace()
    {
        if (_justCalculated || _newInput)
        {
            Clear();
            return;
        }

        if (_currentInput.Length > 1)
            _currentInput = _currentInput[..^1];
        else
            _currentInput = "0";

        UpdateDisplay();
    }

    private void ToggleSign()
    {
        if (_currentInput == "0") return;

        if (_currentInput.StartsWith('-'))
            _currentInput = _currentInput[1..];
        else
            _currentInput = "-" + _currentInput;

        UpdateDisplay();
    }

    private void ApplyPercent()
    {
        if (!double.TryParse(_currentInput.Replace(',', '.'),
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture,
            out double val))
            return;

        double result = val / 100.0;
        _currentInput = FormatResult(result);
        UpdateDisplay();
    }

    private void MainForm_KeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.D0: case Keys.NumPad0: AppendDigit("0"); break;
            case Keys.D1: case Keys.NumPad1: AppendDigit("1"); break;
            case Keys.D2: case Keys.NumPad2: AppendDigit("2"); break;
            case Keys.D3: case Keys.NumPad3: AppendDigit("3"); break;
            case Keys.D4: case Keys.NumPad4: AppendDigit("4"); break;
            case Keys.D5: case Keys.NumPad5: AppendDigit("5"); break;
            case Keys.D6: case Keys.NumPad6: AppendDigit("6"); break;
            case Keys.D7: case Keys.NumPad7: AppendDigit("7"); break;
            case Keys.D8: case Keys.NumPad8: AppendDigit("8"); break;
            case Keys.D9: case Keys.NumPad9: AppendDigit("9"); break;
            case Keys.Decimal: case Keys.OemPeriod: AppendDecimal(); break;
            case Keys.Add: SetOperator('+'); break;
            case Keys.Subtract: case Keys.OemMinus: SetOperator('-'); break;
            case Keys.Multiply: SetOperator('*'); break;
            case Keys.Divide: case Keys.OemQuestion: SetOperator('/'); break;
            case Keys.Enter: Calculate(); break;
            case Keys.Escape: Clear(); break;
            case Keys.Back: Backspace(); break;
        }
        e.Handled = true;
    }

    private void UpdateDisplay()
    {
        lblDisplay.Text = _currentInput;

        // Shrink font for long numbers
        int len = _currentInput.Length;
        lblDisplay.Font = len > 14
            ? new Font("Segoe UI", 16F, FontStyle.Bold)
            : len > 10
                ? new Font("Segoe UI", 22F, FontStyle.Bold)
                : new Font("Segoe UI", 28F, FontStyle.Bold);
    }

    private void AddToHistory(string entry)
    {
        listHistory.Items.Insert(0, entry);
        if (listHistory.Items.Count > 10)
            listHistory.Items.RemoveAt(listHistory.Items.Count - 1);
    }

    private static string FormatResult(double value)
    {
        // Max 10 decimals, no trailing zeros, use comma as decimal separator
        string formatted = value.ToString("G10", System.Globalization.CultureInfo.InvariantCulture);
        return formatted.Replace('.', ',');
    }

    private static string DisplayOp(char op) => op switch
    {
        '+' => "+",
        '-' => "−",
        '*' => "×",
        '/' => "÷",
        _ => op.ToString()
    };
}
