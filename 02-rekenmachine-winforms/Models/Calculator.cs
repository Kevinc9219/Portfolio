namespace RekenmachineWinForms.Models;

public class Calculator
{
    private double _firstOperand;
    private double _secondOperand;
    private char _operator;
    private bool _hasOperator;

    public void SetOperand(double value)
    {
        if (_hasOperator)
            _secondOperand = value;
        else
            _firstOperand = value;
    }

    public void SetOperator(char op)
    {
        _operator = op;
        _hasOperator = true;
    }

    public (double result, string expression) Calculate(double currentValue)
    {
        _secondOperand = currentValue;

        string expression = $"{FormatNumber(_firstOperand)} {_operator} {FormatNumber(_secondOperand)} =";

        double result = _operator switch
        {
            '+' => _firstOperand + _secondOperand,
            '-' => _firstOperand - _secondOperand,
            '*' => _firstOperand * _secondOperand,
            '/' when _secondOperand == 0 => throw new DivideByZeroException(),
            '/' => _firstOperand / _secondOperand,
            _ => throw new InvalidOperationException("Onbekende operator")
        };

        return (result, expression);
    }

    public void Clear()
    {
        _firstOperand = 0;
        _secondOperand = 0;
        _operator = '\0';
        _hasOperator = false;
    }

    public bool HasOperator => _hasOperator;

    public double FirstOperand => _firstOperand;

    public static string FormatNumber(double value)
    {
        return value.ToString("G10");
    }
}
