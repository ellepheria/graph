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

    public static List<Node> FindShortestPath(this Graph graph, Node start, Node end)
    {
        var track = new Dictionary<Node, Node>();
        var queue = new Queue<Node>();
        var endIsFound = false;
        queue.Enqueue(start);
        track[start] = null;
        while (queue.Count != 0)
        {
            var node = queue.Dequeue();
            track.ContainsKey(node);

            foreach (var nextNode in node.IncidentNodes)
            {
                if (track.ContainsKey(nextNode)) continue;
                track[nextNode] = node;
                queue.Enqueue(nextNode);
                if (nextNode == end) endIsFound = true;
            }

            if (endIsFound) break;
        }
        if (!track.ContainsKey(end)) return null;
        var list = new List<Node>();
        while (end != null)
        {
            list.Add(end);
            end = track[end];
        }

        list.Reverse();
        return list;
    }
}