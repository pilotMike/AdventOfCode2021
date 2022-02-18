namespace AdventOfCode.Lib;

public static class StringExtensions
{
    public static IEnumerable<int> SplitParse(this string source, string separator = "\r\n")
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
}