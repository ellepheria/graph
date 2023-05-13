public static class GraphExtensions
{
    public static IEnumerable<IEnumerable<Node>> FindConnectedComponents(this Graph graph)
    {
        var marked = new HashSet<Node>();
        while (true)
        {
            var node = graph.Nodes.Where(z => !marked.Contains(z)).FirstOrDefault();
            if (node == null) break;
            var breadthSearch = node.BreadthSearch();
            foreach (var visitedNode in breadthSearch)
                marked.Add(visitedNode);
            yield return breadthSearch;
        }
    }
}