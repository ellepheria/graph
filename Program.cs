public class Program
{
    public static void Main()
    {
        var graph = Graph.MakeGraph(
            1, 2,
            3, 4,
            3, 5,
            4, 5
        );

        foreach (var component in graph.FindConnectedComponents())
        {
            foreach (var node in component)
                Console.WriteLine(node);
            Console.WriteLine();
        }
    }
}