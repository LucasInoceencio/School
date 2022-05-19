namespace School.Domain.ValueObjects;

public struct Cnpj
{
    private readonly string _value;
    private readonly string _onlyDigitsCnpj;
    public readonly bool IsValid;
    static readonly int[] Multiplier1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
    static readonly int[] Multiplier2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

    private Cnpj(string value)
    {
        _value = value;
        _onlyDigitsCnpj = string.Empty;

        if (value == null)
        {
            IsValid = false;
            return;
        }

        var sameDigits = true;
        var lastDigit = -1;
        var position = 0;
        var totalDigit1 = 0;
        var totalDigit2 = 0;

        foreach (var c in _value)
        {
            if (char.IsDigit(c))
            {
                _onlyDigitsCnpj += c;
                var digito = c - '0';
                if (position != 0 && lastDigit != digito)
                {
                    sameDigits = false;
                }

                lastDigit = digito;
                if (position < 12)
                {
                    totalDigit1 += digito * Multiplier1[position];
                    totalDigit2 += digito * Multiplier2[position];
                }
                else if (position == 12)
                {
                    var dv1 = (totalDigit1 % 11);
                    dv1 = dv1 < 2
                        ? 0
                        : 11 - dv1;

                    if (digito != dv1)
                    {
                        IsValid = false;
                        return;
                    }

                    totalDigit2 += dv1 * Multiplier2[12];
                }
                else if (position == 13)
                {
                    var dv2 = (totalDigit2 % 11);

                    dv2 = dv2 < 2
                        ? 0
                        : 11 - dv2;

                    if (digito != dv2)
                    {
                        IsValid = false;
                        return;
                    }
                }

                position++;
            }
        }

        IsValid = (position == 14) && !sameDigits;
    }

    public static implicit operator Cnpj(string value)
        => new(value);

    public override string ToString()
    {
        return _value;
    }

    public static bool ValidateCnpj(Cnpj cnpj)
    {
        return cnpj.IsValid;
    }

    public string CnpjFormated
        => Convert.ToUInt64(_onlyDigitsCnpj).ToString(@"00\.000\.000\/0000\-00");
}