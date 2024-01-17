using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    // a HashSet is a collection that stores unique elements, meaning each element can appear only once in the collection. It does not allow duplicate elements.  
    public static HashSet<Vector3Int> SimpleRandomWalk(Vector3Int startPosition, int walkLength)
    {
        HashSet<Vector3Int> path = new()
        {
            startPosition
        };
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction3D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }

        return path;
    }


    public static (HashSet<Vector3Int>, Vector3Int) WalkRandomCorridor(Vector3Int startPosition, int corridorLength, Vector3Int lastDirection)
    {
        HashSet<Vector3Int> path = new() {
            startPosition
        };
        var previousPosition = startPosition;
        Vector3Int direction = Direction3D.GetRandomCardinalDirection();

        while (direction == -lastDirection)
        {
            // so it does not walk back
            direction = Direction3D.GetRandomCardinalDirection();
        }

        for (int i = 0; i < corridorLength; i++)
        {


            var newPosition = previousPosition + direction;
            path.Add(newPosition);

            // corridorWidth 2:
            if (direction == Vector3.right || direction == Vector3.left)
            {
                path.Add(new Vector3Int(newPosition.x, newPosition.y, newPosition.z + 1));

            }

            if (direction == Vector3.forward || direction == Vector3.back)
            {
                // up or down
                // path.Add(newPosition + new Vector3Int(-1, 0, 0));
                path.Add(new Vector3Int(newPosition.x + 1, newPosition.y, newPosition.z));
            }

            previousPosition = newPosition;
        }

        return (path, direction);
    }


    public static HashSet<Vector3Int> RunRandomWalk(Vector3Int startPosition, int iterations = 1, int walkLength = 1, bool startRandomlyEachIteration = false)
    {
        if (startPosition == null)
        {
            startPosition = Vector3Int.zero;
        }
        var currentPosition = startPosition;

        HashSet<Vector3Int> floorPositions = new();

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


    // only support 3, 5 etc
    public static HashSet<Vector3Int> CreateRoom(Vector3Int middlePosition, int xSize, int zSize)
    {
        // if xSize = 3; ->  -(3-1)/2 =    and xMax = (3-1)/2
        int xMin = -(xSize - 1) / 2;
        int xMax = (xSize - 1) / 2;
        // if (xSize %2 != 0) { 
        //     // odd, so do x
        // }


        int zMin = -(zSize - 1) / 2;
        int zMax = (zSize - 1) / 2;


        HashSet<Vector3Int> path = new();
        for (int z = zMin; z <= zMax; z++)
        {
            for (int x = xMin; x <= xMax; x++)
            {
                path.Add(new Vector3Int(middlePosition.x + x, 0, middlePosition.z + z));
            }
        }

        return path;
    }

}
