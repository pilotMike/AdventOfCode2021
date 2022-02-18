using System.Collections;

namespace AdventOfCode.Lib;

public readonly record struct Line(Point2D Start, Point2D End) : IEnumerable<Point2D>
{
    public bool IsHorizontal => Start.Y == End.Y;
    public bool IsVertical => Start.X == End.X;

    
    public IEnumerator<Point2D> GetEnumerator() => GetPoints().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private IEnumerable<Point2D> GetPoints()
    {
        //http://ericw.ca/notes/bresenhams-line-algorithm-in-csharp.html
        
        int x0, y0, x1, y1;
        (x0, y0) = Start;
        (x1, y1) = End;
        
        bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
        if (steep)
        {
            int t;
            t = x0; // swap x0 and y0
            x0 = y0;
            y0 = t;
            t = x1; // swap x1 and y1
            x1 = y1;
            y1 = t;
        }
        if (x0 > x1)
        {
            int t;
            t = x0; // swap x0 and x1
            x0 = x1;
            x1 = t;
            t = y0; // swap y0 and y1
            y0 = y1;
            y1 = t;
        }
        int dx = x1 - x0;
        int dy = Math.Abs(y1 - y0);
        int error = dx / 2;
        int ystep = (y0 < y1) ? 1 : -1;
        int y = y0;
        for (int x = x0; x <= x1; x++)
        {
            yield return new Point2D((steep ? y : x), (steep ? x : y));
            error = error - dy;
            if (error < 0)
            {
                y += ystep;
                error += dx;
            }
        }
        yield break;
    }

}