using System.Collections.Generic;
using UnityEngine;

public class Astar
{
    private Status[,] map;
    private int width;
    private int height;

    private class Node
    {
        public Vector2Int Position;
        public Node Parent;
        public float G;
        public float H;
        public float F => G + H;

        public Node(Vector2Int position, Node parent, float g, float h)
        {
            Position = position;
            Parent = parent;
            G = g;
            H = h;
        }
    }

    public void Initialize(Status[,] map)
    {
        this.map = map;
        width = map.GetLength(0);
        height = map.GetLength(1);
    }

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
    {
        var openList = new List<Node>();
        var closedList = new HashSet<Vector2Int>();
        var startNode = new Node(start, null, 0, GetHeuristic(start, goal));
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            var currentNode = GetLowestFScoreNode(openList);
            if (currentNode.Position == goal)
            {
                return RetracePath(currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode.Position);

            foreach (var neighbor in GetNeighbors(currentNode.Position))
            {
                if (closedList.Contains(neighbor) || map[neighbor.x, neighbor.y] == Status.Wall)
                {
                    continue;
                }

                float tentativeG = currentNode.G + 1; // Assume cost between nodes is 1

                var existingNode = openList.Find(node => node.Position == neighbor);
                if (existingNode == null)
                {
                    var h = GetHeuristic(neighbor, goal);
                    var node = new Node(neighbor, currentNode, tentativeG, h);
                    openList.Add(node);
                }
                else if (tentativeG < existingNode.G)
                {
                    existingNode.G = tentativeG;
                    existingNode.Parent = currentNode;
                }
            }
        }

        return new List<Vector2Int>(); // Return an empty path if no path found
    }

    private Node GetLowestFScoreNode(List<Node> nodes)
    {
        Node lowestFNode = nodes[0];
        foreach (var node in nodes)
        {
            if (node.F < lowestFNode.F)
            {
                lowestFNode = node;
            }
        }
        return lowestFNode;
    }

    private List<Vector2Int> RetracePath(Node endNode)
    {
        var path = new List<Vector2Int>();
        var currentNode = endNode;
        while (currentNode != null)
        {
            path.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }
        path.Reverse();
        return path;
    }

    private float GetHeuristic(Vector2Int from, Vector2Int to)
    {
        return Mathf.Abs(from.x - to.x) + Mathf.Abs(from.y - to.y); // Manhattan distance
    }

    private List<Vector2Int> GetNeighbors(Vector2Int position)
    {
        var neighbors = new List<Vector2Int>();

        if (position.x > 0) neighbors.Add(new Vector2Int(position.x - 1, position.y));
        if (position.x < width - 1) neighbors.Add(new Vector2Int(position.x + 1, position.y));
        if (position.y > 0) neighbors.Add(new Vector2Int(position.x, position.y - 1));
        if (position.y < height - 1) neighbors.Add(new Vector2Int(position.x, position.y + 1));

        return neighbors;
    }
}