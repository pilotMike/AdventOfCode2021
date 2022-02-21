using System.Numerics;

namespace AdventOfCode.Lib;

public static class Numbers
{
    /// <summary>
    /// Returns the sum of all numbers up to n
    /// </summary>
    public static int NaturalSum(int n) => n * (n + 1) / 2;

    /// <summary>
    /// Returns the sum of all numbers in a range.
    /// </summary>
    public static int NaturalSum(int min, int max) => NaturalSum(max) - NaturalSum(min - 1);
    
    /// <summary>
    /// Returns the sum of all numbers up to n
    /// </summary>
    public static BigInteger NaturalSum(BigInteger n) => n * (n + 1) / 2;

    /// <summary>
    /// Returns the sum of all numbers in a range.
    /// </summary>
    public static BigInteger NaturalSum(BigInteger min, BigInteger max) => NaturalSum(max) - NaturalSum(min - 1);
}