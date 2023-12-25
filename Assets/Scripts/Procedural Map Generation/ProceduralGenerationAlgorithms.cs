using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    // a HashSet is a collection that stores unique elements, meaning each element can appear only once in the collection. It does not allow duplicate elements.  
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new()
        {
            startPosition
        };
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path;
    }


    public static HashSet<Vector2Int> WalkRandomCorridor(Vector2Int startPosition, int corridorLength)
    {
        HashSet<Vector2Int> path = new() {
            startPosition
        };
        var previousPosition = startPosition;
        Vector2Int direction = Direction2D.GetRandomCardinalDirection();

        for (int i = 0; i < corridorLength; i++)
        {
            var newPosition = previousPosition + direction;
            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path;
    }


    public static HashSet<Vector2Int> RunRandomWalk(Vector2Int startPosition, int iterations = 1, int walkLength = 1, bool startRandomlyEachIteration = false)
    {
        if (startPosition == null)
        {
            startPosition = Vector2Int.zero;
        }
        var currentPosition = startPosition;

        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < iterations; i++)
        {
            var path = SimpleRandomWalk(currentPosition, walkLength);
            floorPositions.UnionWith(path); // add them but remove duplicates

            if (startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }

        return floorPositions;
    }

    public static HashSet<Vector2Int> CreateRoom(Vector2Int currentPosition)
    {
        int xMax = Random.Range(10, 12);
        int yMax = Random.Range(10, 12);

        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        for (int x = -1; x <= xMax; x++)
        {
            for (int y = -1; y <= yMax; y++)
            {
                path.Add(new Vector2Int(currentPosition.x + x, currentPosition.y + y));
            }
        }

        return path;
    }

}
