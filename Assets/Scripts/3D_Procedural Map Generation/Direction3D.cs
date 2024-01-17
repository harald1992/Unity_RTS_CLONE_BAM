using System.Collections.Generic;
using UnityEngine;

public static class Direction3D
{
    // all 4 possible directions
    public static List<Vector3Int> cardinalDirectionsList = new()
    {
        new Vector3Int(0, 0, 1), // up
        new Vector3Int(1, 0, 0), // right
        new Vector3Int(0, 0, -1), // down
        new Vector3Int(-1, 0, 0), // left
    };

    public static List<Vector3Int> diagonalDirectionsList = new()
    {
        new Vector3Int(1, 0, 1),
        new Vector3Int(1, 0, -1),
        new Vector3Int(-1, 0, -1),
        new Vector3Int(-1, 0, 1),
    };

    public static List<Vector3Int> eightDirectionsList = new()
    {
        new Vector3Int(0, 0, 1),
        new Vector3Int(1, 0, 1),
        new Vector3Int(1, 0, 0),
        new Vector3Int(1, 0, -1),
        new Vector3Int(0, 0, -1),
        new Vector3Int(-1, 0, -1),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(-1, 0, 1),
    };

    public static Vector3Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}