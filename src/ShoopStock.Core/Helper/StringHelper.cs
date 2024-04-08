using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;

namespace ShoopStock.Core.Helper;

public static class StringHelper
{
    public static string BuildContentKey(string dir, string key, string fileName, string name, string? ext = null)
    {
        if (ext != null && !ext.StartsWith("."))
        {
            ext = "." + ext;
        }

        ext = ext ?? Path.GetExtension(fileName);
        key = $"{key}/{name}";
        return !string.IsNullOrEmpty(dir)
            ? $"{dir}/{key}{ext}"
            : $"{key}{ext}";
    }

    public static string GetRandomString(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private static string EncodeString(this string input)
    {
        using var sha256Hash = SHA256.Create();

        var data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        var sBuilder = new StringBuilder();

        foreach (var b in data)
        {
            sBuilder.Append(b.ToString("x2"));
        }

        var encodeString = sBuilder.ToString();

        return encodeString;
    }

    public static string[] SplitErrorMassage(string str)
    {
        str = str[3..];

        char[] separator = { '_', ' ' };

        var strings = str.Split(separator, StringSplitOptions.None);

        return strings;
    }

    public static string SplitUpperCase(string str)
    {
        var strings = Regex.Split(str, @"(?<!^)(?=[A-Z])");

        return strings.Aggregate("", (current, tmp) => current + (tmp + " "));
    }

    public static string SplitPhoneNumber(string s)
    {
        char[] separator = { '+' };
        var str = s.Split(separator, StringSplitOptions.None);
        return str.Length < 2 ? "0" : str[1];
    }
}
