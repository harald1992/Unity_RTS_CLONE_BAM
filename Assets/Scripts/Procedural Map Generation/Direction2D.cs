using System.Collections.Generic;
using UnityEngine;

public static class Direction2D
{

    // all 4 possible directions
    public static List<Vector2Int> cardinalDirectionsList = new()
    {
        new Vector2Int(0,1), // up
        new Vector2Int(1,0), // right
        new Vector2Int(0,-1), // down
        new Vector2Int(-1,0), // left
    };

    public static List<Vector2Int> diagonalDirectionsList = new()
    {
        new Vector2Int(1,1),
        new Vector2Int(1,-1),
        new Vector2Int(-1,-1),
        new Vector2Int(-1,1),
    };

    public static List<Vector2Int> eightDirectionsList = new()
    {
        new Vector2Int(0,1),
        new Vector2Int(1,1),
        new Vector2Int(1,0),
        new Vector2Int(1,-1),
        new Vector2Int(0,-1),
        new Vector2Int(-1,-1),
        new Vector2Int(-1,0),
        new Vector2Int(-1,1),
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}