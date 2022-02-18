using AdventOfCode.DailyChallenges.Day04;
using NUnit.Framework;

namespace Tests.Day04.Tests;

public class BingBoardTests
{
    [Test]
    public void Columns_ReturnsCellsInCorrectOrder()
    {
        var cols = Parse(input).Columns();
        var rows = input.Split("\r\n");

        Func<int, int[]> getCol = c => rows.Select(s =>
        {
            return int.Parse(s.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[c]);
        }).ToArray();
        
        foreach (var (col, c) in cols.Select((col, c) => (col, c)))
        {
            var textColumn = getCol(c);
            var mismatch = textColumn.Zip(col, (a, b) => a != b.Value).Any<bool>(b => b);
            Assert.IsFalse(mismatch);
        }
    }

    [Test]
    public void Rows_ReturnsCells_InCorrectOrder()
    {
        var bingoRows = Parse(input).Rows();
        var rows = input.Split("\r\n");

        foreach (var (r, i) in bingoRows.Select((row, i) => (row, i)))
        {
            var rowText = rows[i].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse);
            var mismatch = rowText.Zip(r, (a, b) => a != b.Value).Any<bool>(b => b);
            Assert.IsFalse(mismatch);
        }
    }

    BingoBoard Parse(string input)
    {
        var res = input.Split("\r\n")
            .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(nums => new BingoCell(int.Parse(nums))));
        var board = BingoBoard.Fill(res);
        return board;
    }

    private const string input = @"22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19";
}