using System.Globalization;

namespace RealBoxNetCls;
public static class Extensions
{
    public static string ToCpf(this string t)
    {
        int n = 0;
        string Mask = "___.___.___-__";
        char[] s = Mask.ToCharArray();

        for (int i = 0; i < s.Length; i++)
            if (Mask[i] == '_')
                if (n < t.Length) s[i] = t[n++]; else break;

        return new string(s);
    }
    public static string ToCnpj(this string t)
    {
        int n = 0;
        string Mask = "__.___.___/____-__";
        char[] s = Mask.ToCharArray();

        for (int i = 0; i < s.Length; i++)
            if (Mask[i] == '_')
                if (n < t.Length) s[i] = t[n++]; else break;

        return new string(s);
    }
    public static string ToDigits(this string s)
    {
        return new(s[..].Where(c => char.IsDigit(c)).ToArray());
    }
    public static decimal ToDecimal(this string s)
    {
        NumberStyles Ns = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign;
        return string.IsNullOrEmpty(s) ? 0 : decimal.Parse(s, Ns);
    }
}