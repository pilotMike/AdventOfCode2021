namespace AdventOfCode.Lib;

public static class StringExtensions
{
    private const string NewLine = "\r\n";
    
    public static IEnumerable<int> SplitParse(this string source, string separator = NewLine)
    {
        for (int i = 0; i < source.Length;)
        {
            var length = source.AsSpan()[i..].IndexOf(separator);
            if (length < 0) length = source.Length - i;
            
            var sub = source.AsSpan().Slice(i, length).Trim();
            var num = int.Parse(sub);
            yield return num;

            i += length + separator.Length;
        }
    }

    public delegate T SpanFactoryFunc<T>(ReadOnlySpan<char> span);
    public static IEnumerable<T> SplitParse<T>(this string source, SpanFactoryFunc<T> func, string separator = NewLine)
    {
        for (int i = 0; i < source.Length;)
        {
            var length = source.AsSpan()[i..].IndexOf(separator);
            if (length < 0) length = source.Length - i;
            
            var sub = source.AsSpan().Slice(i, length).Trim();
            var val = func(sub);
            yield return val;

            i += length + separator.Length;
        }
    }
    
    // public static List<T> SplitParse<T>(this ReadOnlySpan<char> source, SpanFactoryFunc<T> func, string separator = NewLine)
    // {
    //     List<T> output = new List<T>();
    //     for (int i = 0; i < source.Length;)
    //     {
    //         var length = source[i..].IndexOf(separator);
    //         if (length < 0) length = source.Length - i;
    //         
    //         var sub = source.Slice(i, length).Trim();
    //         var val = func(sub);
    //         output.Add(val);
    //
    //         i += length + separator.Length;
    //     }
    //
    //     return output;
    // }

    // public static string StringJoin(this IEnumerable<string> strings, string? join = null) => string.Join(join, strings);

    public static string StringJoin(this IEnumerable<char> strings, string? join = null) =>
        string.Join(join, strings);
}