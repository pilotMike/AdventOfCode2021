namespace AdventOfCode.DailyChallenges.Day04;

public readonly record struct BingoCell(bool Marked, int Value)
{
    public BingoCell(int value) : this(false, value){}
}
public class BingoBoard
{
    private readonly BingoCell[,] _cells = new BingoCell[5, 5];

    public IEnumerable<IEnumerable<BingoCell>> Columns()
    {
        for (int i = 0; i < _cells.GetUpperBound(0) + 1; i++)
        {
            BingoCell[] col = new BingoCell[_cells.GetUpperBound(1) + 1];
            for (int j = 0; j < _cells.GetUpperBound(1) + 1; j++)
            {
                col[j] = _cells[j,i];
            }

            yield return col;
        }
    }
    
    public IEnumerable<IEnumerable<BingoCell>> Rows()
    {
        for (int i = 0; i < _cells.GetUpperBound(0) + 1; i++)
        {
            BingoCell[] row = new BingoCell[_cells.GetUpperBound(0) + 1];
            for (int j = 0; j < _cells.GetUpperBound(1) + 1; j++)
            {
                row[j] = _cells[i,j];
            }

            yield return row;
        }
    }

    public IEnumerable<BingoCell> AllCells() => _cells.Cast<BingoCell>();

    public void MarkChecked(int value)
    {
        for (int row = 0; row < _cells.GetUpperBound(0) + 1; row++)
        {
            for (int col = 0; col < _cells.GetUpperBound(1) + 1; col++)
            {
                var cell = _cells[row, col];
                if (cell.Value == value)
                {
                    _cells[row, col] = new BingoCell(true, cell.Value);
                    return;
                }
            }
        }
    }

    public bool IsWinner()
    {
        // check all columns and rows for all being checked
        return Rows().Any(r => r.All(x => x.Marked)) || Columns().Any(c => c.All(x => x.Marked));
    }

    public static BingoBoard Fill(IEnumerable<IEnumerable<BingoCell>> res)
    {
        var bb = new BingoBoard();
        int r = 0;
        foreach (var row in res)
        {
            int c = 0;
            foreach (var col in row)
            {
                bb._cells[r, c] = col;
                c++;
            }
            r++;
        }

        return bb;
    }
}