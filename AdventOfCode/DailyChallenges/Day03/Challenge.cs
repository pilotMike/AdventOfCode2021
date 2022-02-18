using AdventOfCode.Lib;

namespace AdventOfCode.DailyChallenges.Day03;

public class Challenge
{
    readonly record struct PowerConsumption(int Value)
    {
        public static PowerConsumption From(GammaRate gammaRate, EpsilonRate epsilonRate) =>
            new PowerConsumption(gammaRate.Value * epsilonRate.Value);
    }

    readonly record struct GammaRate(int Value);

    readonly record struct EpsilonRate(int Value);

    readonly partial record struct DiagnosticReport(IReadOnlyList<string> BinaryRows)
    {
        // used for part 1
        public IEnumerable<IEnumerable<char>> ColumnsOfBits()
        {
            if (BinaryRows == null) throw new NullReferenceException(nameof(BinaryRows));
            var length = BinaryRows.Count;
            var width = BinaryRows[0].Length;

            char[] buffer = new char[length];
            for (int column = 0; column < width; column++)
            {
                for (int row = 0; row < length; row++)
                {
                    buffer[row] = BinaryRows[row][column];
                }

                yield return buffer;
            }
        }
    }

    class GammaAndEpsilonRateCalculator
    {
        public static (GammaRate gammaRate, EpsilonRate epsilonRate) Calculate(DiagnosticReport dr)
        {
            var gammaRateBits = dr.ColumnsOfBits()
                .Select(bits => bits.GroupBy(b => b).MaxBy(g => g.Count()).Key)
                .StringJoin();

            char[] epsilonBits = new char[gammaRateBits.Length];
            for (int i = 0; i < gammaRateBits.Length; i++)
            {
                epsilonBits[i] = gammaRateBits[i] == '1' ? '0' : '1';
            }

            var gammaRate = Convert.ToInt32(gammaRateBits, 2);
            var epsilonRate = Convert.ToInt32(new string(epsilonBits), 2);

            return (new GammaRate(gammaRate), new EpsilonRate(epsilonRate));
        }
    }

    public class Part1 : IChallenge
    {
        public string DefaultInput => Input.Value;
        public ChallengeResult Execute(string? input = null)
        {
            input ??= DefaultInput;

            var (gammaRate, epsilonRate) = GammaAndEpsilonRateCalculator.Calculate(
                new DiagnosticReport(input.Split("\r\n")));

            var pc = PowerConsumption.From(gammaRate, epsilonRate);

            return new ValueChallengeResult<int>(pc.Value);
        }
    }

    readonly record struct OxygenGeneratorRating(int Value);

    readonly record struct Co2ScrubberRating(int Value);

    readonly partial record struct DiagnosticReport
    {
        // used for part 2
        public IEnumerable<(char bit, string row)> Column(int column)
        {
            for (int i = 0; i < BinaryRows.Count; i++)
            {
                yield return (BinaryRows[i][column], BinaryRows[i]);
            }
        }
    }

    abstract class BitCriteria<T>
    {
        protected abstract bool KeepOnes(int zeroCount, int oneCount);
        
        protected string Filter(DiagnosticReport report, int column)
        {
            var grouped = report.Column(column).GroupBy(x => x.bit);
            int zeroCount = 0, oneCount = 0;
            IEnumerable<string> zeros = null, ones = null;
            foreach (var x in grouped)
            {
                if (x.Key == '0')
                {
                    zeroCount = x.Count();
                    zeros = x.Select(y => y.row);
                }
                else if (x.Key == '1')
                {
                    oneCount = x.Count();
                    ones = x.Select(y => y.row);
                }
            }

            // return the rows that are kept
            var keptRows = KeepOnes(zeroCount, oneCount)
                ? ones
                : zeros;
                    
            var dr = new DiagnosticReport(keptRows.ToList());
            if (dr.BinaryRows.Count == 1)
                return dr.BinaryRows.First();

            var row = dr.BinaryRows.First();
            return Filter(dr, column == row.Length ? 0 : column + 1);
        }

        public abstract T Determine(DiagnosticReport diagnosticReport);
    }

    class OxygenGeneratorRatingBitCriteria : BitCriteria<OxygenGeneratorRating>
    {
        public override OxygenGeneratorRating Determine(DiagnosticReport dr)
        {
            var lastValue = Filter(dr, 0);
            var intValue = Convert.ToInt32(lastValue, 2);
            return new OxygenGeneratorRating(intValue);
        }

        protected override bool KeepOnes(int zeroCount, int oneCount) => 
            zeroCount.CompareTo(oneCount) <= 0;
    }

    class Co2ScrubberRatingBitCriteria : BitCriteria<Co2ScrubberRating>
    {
        protected override bool KeepOnes(int zeroCount, int oneCount) => 
            zeroCount.CompareTo(oneCount) > 0;

        public override Co2ScrubberRating Determine(DiagnosticReport diagnosticReport)
        {
            var lastValue = Filter(diagnosticReport, 0);
            var intValue = Convert.ToInt32(lastValue, 2);
            return new Co2ScrubberRating(intValue);
        }
    }
    
    readonly record struct LifeSupportRating(int Value)
    {
        public static LifeSupportRating From(OxygenGeneratorRating ogr, Co2ScrubberRating co2Rating) =>
            new LifeSupportRating(ogr.Value * co2Rating.Value);
    }

    public class Part2 : IChallenge
    {
        public string DefaultInput => Input.Value;
        public ChallengeResult Execute(string? input = null)
        {
            input ??= DefaultInput;
            
            var dr = new DiagnosticReport(input.Split("\r\n"));

            var oxygenGeneratorRating = new OxygenGeneratorRatingBitCriteria().Determine(dr);
            var co2ScrubberRating = new Co2ScrubberRatingBitCriteria().Determine(dr);


            var lifeSupportRating = LifeSupportRating.From(
                oxygenGeneratorRating,
                co2ScrubberRating
            );

            return new ValueChallengeResult<int>(lifeSupportRating.Value);
        }
    }
}