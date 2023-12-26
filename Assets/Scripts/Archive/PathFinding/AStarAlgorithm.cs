using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarAlgorithm
{

    public AStarAlgorithm()
    {
    }

    public HashSet<Vector2> GetPath(Vector2 start, Vector2 goal)
    {
        HashSet<Vector2> openSet = new HashSet<Vector2>();
        HashSet<Vector2> closedSet = new HashSet<Vector2>();

        openSet.Add(start);

        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();

        Dictionary<Vector2, int> gScore = new Dictionary<Vector2, int>();
        gScore[start] = 0;

        Dictionary<Vector2, int> fScore = new Dictionary<Vector2, int>();
        fScore[start] = HeuristicCostEstimate(start, goal);

        while (openSet.Count > 0)
        {
            Vector2 current = GetLowestFScore(openSet, fScore);

            if (current == goal)
                return ReconstructPath(cameFrom, goal);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Vector2 neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                int tentativeGScore = gScore[current] + DistanceBetween(current, neighbor);

                if (!openSet.Contains(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = tentativeGScore + HeuristicCostEstimate(neighbor, goal);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null; // No path found
    }

    int DistanceBetween(Vector2 a, Vector2 b)
    {
        // Replace with your distance calculation method (e.g., Manhattan, Euclidean, etc.)
        return (int)(Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y));
    }

    int HeuristicCostEstimate(Vector2 start, Vector2 goal)
    {
        // Replace with your heuristic function (e.g., Manhattan distance, Euclidean distance, etc.)
        return DistanceBetween(start, goal);
    }

    HashSet<Vector2> GetNeighbors(Vector2 node)
    {
        HashSet<Vector2> neighbors = new HashSet<Vector2>();

        // Replace this with your logic to find neighboring nodes based on your grid
        // Example:
        neighbors.Add(new Vector2(node.x + 1, node.y));
        neighbors.Add(new Vector2(node.x - 1, node.y));
        neighbors.Add(new Vector2(node.x, node.y + 1));
        neighbors.Add(new Vector2(node.x, node.y - 1));


        // added
        neighbors.Add(new Vector2(node.x + 1, node.y + 1));
        neighbors.Add(new Vector2(node.x - 1, node.y - 1));
        neighbors.Add(new Vector2(node.x + 1, node.y - 1));
        neighbors.Add(new Vector2(node.x - 1, node.y + 1));

        return neighbors;
    }

    Vector2 GetLowestFScore(HashSet<Vector2> openSet, Dictionary<Vector2, int> fScore)
    {
        int lowestFScore = int.MaxValue;
        Vector2 lowestNode = Vector2.zero;

        foreach (Vector2 node in openSet)
        {
            if (fScore.TryGetValue(node, out int score) && score < lowestFScore)
            {
                lowestFScore = score;
                lowestNode = node;
            }
        }

        return lowestNode;
    }

    // HashSet<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current)
    // {
    //     HashSet<Vector2> path = new HashSet<Vector2>();

    //     while (cameFrom.ContainsKey(current))
    //     {
    //         path.Add(current);
    //         current = cameFrom[current];
    //     }

    //     path.Add(current); // Add the start node
    //     path.Reverse(); // Reverse the list

    //     return path;
    // }

    HashSet<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current)
    {
        List<Vector2> pathList = new List<Vector2>();

        while (cameFrom.ContainsKey(current))
        {
            pathList.Add(current);
            current = cameFrom[current];
        }

        pathList.Add(current); // Add the start node

        pathList.Reverse(); // Reverse the list

        return new HashSet<Vector2>(pathList);
    }


}
