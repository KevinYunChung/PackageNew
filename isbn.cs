private static bool IsValidIsbn13(string isbn13)
{
    bool result = false;
    if (!string.IsNullOrEmpty(isbn13))
    {
        long j;
        if (isbn13.Contains('-')) isbn13 = isbn13.Replace("-", "");
        if (!Int64.TryParse(isbn13, out j))
            result = false;
        int sum = 0;
        for (int i = 0; i < 12; i++)
        {
            sum += Int32.Parse(isbn13[i].ToString()) * (i % 2 == 1 ? 3 : 1);
        }
        int remainder = sum % 10;
        int checkDigit = 10 - remainder;
        if (checkDigit == 10) checkDigit = 0;
        result = (checkDigit == int.Parse(isbn13[12].ToString()));
    }
    return result;
}

private static bool IsValidIsbn10(string isbn10)
{
    bool result = false;
    if (!string.IsNullOrEmpty(isbn10))
    {
        long j;
        if (isbn10.Contains('-')) isbn10 = isbn10.Replace("-", "");
        if (!Int64.TryParse(isbn10.Substring(0, isbn10.Length - 1), out j))
            result = false;
        char lastChar = isbn10[isbn10.Length - 1];
        if (lastChar == 'X' && !Int64.TryParse(lastChar.ToString(), out j))
            result = false;
        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += Int32.Parse(isbn10[i].ToString()) * (i + 1);
        int remainder = sum % 11;
        result = (remainder == int.Parse(isbn10[9].ToString()));
    }
    return result;
}

public static bool CheckISBN10(string number)
{
    number.CheckArgument(nameof(number));

    int mult = 1;
    int result = 0;

    for (int i = 0; i < number.Length - 1; i++)
    {
        result += Convert.ToInt32(number[i].ToString()) * mult;
        mult++;
    }

    result %= 11;

    if (result == 10 && number[number.Length - 1].ToString().ToLower() == "x")
    {
        return true;
    }
    else if (result == Convert.ToInt32(number[number.Length - 1].ToString()))
    {
        return true;
    }

    return false;
}