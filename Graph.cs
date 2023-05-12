public class Edge
{
    public readonly Node First;
    public readonly Node Second;

    public Edge(Node first, Node second)
    {
        First = first;
        Second = second;
    }

    public bool IsIncident(Node node)
    {
        return node == First || node == Second;
    }

    public Node OtherNode(Node node)
    {
        if (First == node) return Second;
        else if (Second == node) return First;
        else throw new ArgumentException();
    }
}

public class Node
{
    private readonly List<Edge> incidentEdges = new();

    public readonly int Number;
    public IEnumerable<Node> IncidentNodes => IncidentEdges.Select(z => z.OtherNode(this));

    public IEnumerable<Edge> IncidentEdges => IncidentEdges.Select(e => e);

    public Node(int number)
    {
        Number = number;
    }

    public void Connect(Node anotherNode, Graph graph)
    {
        var edge = new Edge(this, anotherNode);
        incidentEdges.Add(edge);
        anotherNode.incidentEdges.Add(edge);
    }

    public void Disconnect(Edge edge)
    {
        edge.First.incidentEdges.Remove(edge);
        edge.Second.incidentEdges.Remove(edge);
    }

    public override string ToString()
    {
        return Number.ToString();
    }
}

public class Graph
{
    private readonly Node[] nodes;

    public Graph(int nodesCount)
    {
        nodes = Enumerable
            .Range(0, nodesCount)
            .Select(z => new Node(z))
            .ToArray();
    }

    public Node this[int index] => nodes[index];

    public IEnumerable<Node> Nodes => nodes.Select(n => n);

    public IEnumerable<Edge> Edges
    {
        get
        {
            return Nodes
                .SelectMany(z => z.IncidentEdges)
                .Distinct();
        }
    }

    public void Connect(int v1, int v2)
    {
        nodes[v1].Connect(nodes[v2], this);
    }
}
