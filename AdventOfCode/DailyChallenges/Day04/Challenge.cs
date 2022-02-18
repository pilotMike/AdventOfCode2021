using AdventOfCode.Lib;
using MoreLinq;

namespace AdventOfCode.DailyChallenges.Day04;

public class Challenge
{
    private static BingoBoard[] Parse(string boardText) => boardText.Split("\r\n")
        .Segment(string.IsNullOrWhiteSpace)
        .Select(boardLines =>
        {
            var boardCells =
                boardLines
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                        .Select(num => new BingoCell(int.Parse(num))));
            return BingoBoard.Fill(boardCells);
        })
        .ToArray();
    
    public class Part1 : IChallenge
    {
        public string DefaultInput => Input.BingoBoards;

        public ChallengeResult Execute(string? input = null)
        {
            input ??= DefaultInput;

            var bingoBoards = Parse(input);

            var drawNumbers = Input.DrawNumbers.Split(',').Select(int.Parse);
            foreach (var number in drawNumbers)
            {
                foreach (var board in bingoBoards)
                {
                    board.MarkChecked(number);
                    if (board.IsWinner())
                    {
                        var unmarkedSum = board.AllCells().Where(c => !c.Marked).Sum(c => c.Value);
                        var multiplied = unmarkedSum * number;
                        return new ValueChallengeResult<int>(multiplied);
                    }
                }
            }

            throw new Exception("no winner found");
        }
    }

    public class Part2 : IChallenge
    {
        // let the squid win
        public string DefaultInput => Input.BingoBoards;
        public ChallengeResult Execute(string? input = null)
        {
            input ??= DefaultInput;
            
            var drawNumbers = Input.DrawNumbers.Split(',').Select(int.Parse);
            var bingoBoards = Parse(input);

            var winningBoardNumber = new Dictionary<BingoBoard, int>();
            var winOrder = new List<BingoBoard>();
            foreach (var number in drawNumbers)
            foreach (var board in bingoBoards)
            {
                if (winningBoardNumber.ContainsKey(board))
                    continue;
                board.MarkChecked(number);
                if (board.IsWinner())
                {
                    winningBoardNumber[board] = number;
                    winOrder.Add(board);
                }
            }

            var lastWinner = winOrder.Last();
            var unmarkedSum = lastWinner.AllCells().Where(c => !c.Marked).Sum(c => c.Value);
            var res = unmarkedSum * winningBoardNumber[lastWinner];

            return new ValueChallengeResult<int>(res);
        }
    }
}