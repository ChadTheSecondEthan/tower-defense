/*using UnityEngine;
using System.Collections.Generic;

public class Path_Old
{
    const int MoveStraightCost = 10;
    const int MoveDiagonalCost = 14;

    PathNode[] pathNodes;
    int current;

    public Path_Old(Tile stop)
    {
        current = 0;

        Tile[] tiles = Grid.Instance.AllTiles;
        List<PathNode> openNodes = new();
        List<PathNode> closedNodes = new();
        PathNode[] pathNodes = new PathNode[tiles.Length];

        for (int i = 0; i < tiles.Length; i++)
        {
            pathNodes[i] = new PathNode(tiles[i]);
            pathNodes[i].gCost = int.MaxValue;
            pathNodes[i].CalculateFCost();
            openNodes.Add(pathNodes[i]);
            closedNodes.Add(pathNodes[i]);
        }

        pathNodes[0].gCost = 0;
        pathNodes[0].hCost = CalculateDistanceCost(
            pathNodes[0].tile.transform.position, 
            stop.transform.position);
        pathNodes[0].CalculateFCost();

        while (openNodes.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openNodes);
            if (currentNode.tile == stop)
                CalculatePathNodes(currentNode);

            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);
        }
    }

    void CalculatePathNodes(PathNode end)
    {
        List<PathNode> nodes = new();
        while (end.prev != null)
        {
            nodes.Add(end);
            end = end.prev;
        }
        pathNodes = nodes.ToArray();
    }

    List<PathNode> GetNeighbourNodes(PathNode currentNode)
    {
        List<PathNode> neighbours = new();
        foreach (Tile tile in Grid.Instance.GetNeighbours(currentNode.tile))
            neighbours.Add(new PathNode(tile));
        return neighbours;
    }

    float CalculateDistanceCost(Vector2 a, Vector2 b)
    {
        float xDistance = Mathf.Abs(a.x - b.x);
        float yDistance = Mathf.Abs(a.y - b.y);
        float remaining = Mathf.Abs(xDistance - yDistance);
        return MoveDiagonalCost * Mathf.Min(xDistance, yDistance) + MoveStraightCost * remaining;
    }

    PathNode GetLowestFCostNode(List<PathNode> nodes)
    {
        PathNode lowestCostNode = nodes[0];
        for (int i = 1; i < nodes.Count; i++)
        {
            if (lowestCostNode.fCost > nodes[i].fCost)
                lowestCostNode = nodes[i];
        }
        return lowestCostNode;
    }

    public Tile currentTile() => pathNodes[current].tile;
    public void next() => current++;
}*/