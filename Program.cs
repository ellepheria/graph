public class Program
{
    public static void Main()
    {
        var graph = Graph.MakeGraph(
            0, 1,
            0, 2,
            1, 3,
            1, 4,
            2, 3,
            2, 4
        );

        foreach (var e in graph[0].BreadthSearch())
            Console.WriteLine(e);
        foreach (var e in graph[0].DepthSearch())
            Console.WriteLine(e);
    }
}