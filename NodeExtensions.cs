public static class NodeExtensions
{
    public static IEnumerable<Node> BreadthSearch(this Node startNode)
    {
        var visited = new HashSet<Node>();
        var queue = new Queue<Node>();
        queue.Enqueue(startNode);
        while (queue.Count != 0)
        {
            var node = queue.Dequeue();
            if (visited.Contains(node)) continue;
            visited.Add(node);
            yield return node;
            foreach (var incidentNode in node.IncidentNodes)
                queue.Enqueue(incidentNode);
        }
    }
}