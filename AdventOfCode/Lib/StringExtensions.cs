namespace AdventOfCode.Lib;

public static class StringExtensions
{
    private const string NewLine = "\r\n";
    
    public static IEnumerable<int> SplitParse(this string source)
    {
        for (int i = 0; i < source.Length;)
        {
            var length = source.AsSpan()[i..].IndexOf(NewLine);
            if (length < 0) length = source.Length - i;
            
            var sub = source.AsSpan().Slice(i, length).Trim();
            var num = int.Parse(sub);
            yield return num;

            i += length + NewLine.Length;
        }
    }

    public delegate T SpanFactoryFunc<T>(ReadOnlySpan<char> span);
    public static IEnumerable<T> SplitParse<T>(this string source, SpanFactoryFunc<T> func)
    {
        for (int i = 0; i < source.Length;)
        {
            var length = source.AsSpan()[i..].IndexOf(NewLine);
            if (length < 0) length = source.Length - i;
            
            var sub = source.AsSpan().Slice(i, length).Trim();
            var val = func(sub);
            yield return val;

            i += length + NewLine.Length;
        }
    }

    // public static string StringJoin(this IEnumerable<string> strings, string? join = null) => string.Join(join, strings);

    public static string StringJoin(this IEnumerable<char> strings, string? join = null) =>
        string.Join(join, strings);
}