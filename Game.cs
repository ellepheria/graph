﻿public class Game
{
    private const int Size = 3;
    public int[,] Data { get; set; }

    public Game(int[,] data)
    {
        Data = data;
    }

    public Game(Game original)
        : this((int[,])original.Data.Clone())
    {
        
    }

    public Game Move(int dx, int dy)
    {
        var point = GamePoints
            .Where(p => Data[p.X, p.Y] == 0)
            .Where(p => PointInRectangle(p, dx, dy))
            .FirstOrDefault();
        if (point == null) return null;

        var newGame = new Game(this);
        newGame.Data[point.X, point.Y] = Data[point.X + dx, point.Y + dy];
        newGame.Data[point.X + dx, point.Y + dy] = Data[point.X, point.Y];
        return newGame;
    }

    public IEnumerable<Game> AllAdjacentGames() =>
        Rectangle(-1, 1, -1, 1)
            .Where(point => point.X == 0 || point.Y == 0)
            .Select(point => Move(point.X, point.Y))
            .Where(game => game != null);

    public override bool Equals(object? obj)
    {
        var game = obj as Game;
        return GamePoints
            .All(point => Data[point.X, point.Y] == game.Data[point.X, point.Y]);
    }

    public override int GetHashCode() =>
        GamePoints
            .Select(point => Data[point.X, point.Y])
            .Aggregate((sum, val) => sum * 97 + val);

    public void Print()
    {
        var str = GamePoints
            .GroupBy(z => z.X)
            .Select(row => row
                .Select(point => Data[point.X, point.Y].ToString()).Aggregate((a, b) => a + " " + b))
            .Aggregate((a, b) => a + "\n" + b);
        Console.WriteLine(str);
        Console.WriteLine();
    }

    public static void Resolve(Game start, Game target)
    {
        var path = new Dictionary<Game, Game>();
        path[start] = null;
        var queue = new Queue<Game>();
        queue.Enqueue(start);
        while (queue.Count != 0)
        {
            var game = queue.Dequeue();

            var nextGames = game
                .AllAdjacentGames()
                .Where(g => !path.ContainsKey(g));
            foreach (var nextGame in nextGames)
            {
                path[nextGame] = game;
                queue.Enqueue(nextGame);
            }
            if (path.ContainsKey(target)) break;
        }
        while (target != null)
        {
            target.Print();
            target = path[target];
        }
    }

    private bool PointInRectangle(Point p, int dx, int dy) =>
        p.X + dx >= 0 && p.X + dx < Size && p.Y + dy >= 0 && p.Y + dy < Size;
    
    private IEnumerable<Point> Rectangle(int xmin, int xmax, int ymin, int ymax)
    {
        for (var x = xmin; x <= xmax; x++)
        for (var y = ymin; y <= ymax; y++)
            yield return new Point(x, y);
    }

    private IEnumerable<Point> GamePoints =>
        Rectangle(0, Size - 1, 0, Size - 1);
}

public class Point
{
    public readonly int X;
    public readonly int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}