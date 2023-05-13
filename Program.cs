public class Program
{
    public static void Main()
    {
        var start = new Game(new[,] {
            {4, 1, 3},
            {7, 2, 6},
            {5, 0, 8}
        });

        var target = new Game(new[,]{
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 0}
        });
        
        Game.Resolve(start, target);
    }
}