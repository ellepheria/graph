public class Program
{
    public static void Main()
    {
        var graph = Graph.MakeGraph(
            0, 1,
            1, 4,
            0, 2,
            2, 3,
            3, 4
        );

        foreach (var node in graph.FindShortestPath(graph[0], graph[4]))
            Console.WriteLine(node);
    }
}