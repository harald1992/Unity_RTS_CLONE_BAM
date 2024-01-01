using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class WallGenerator : MonoBehaviour
{

    public void CreateWalls(HashSet<Vector2Int> floorPositions, TerrainCreator terrainCreator)
    {
        if (terrainCreator == null)
        {
            terrainCreator = GameObject.FindWithTag("TerrainCreator").GetComponent<TerrainCreator>();
            terrainCreator.PaintAllCorridors(floorPositions);
        }



        HashSet<Vector2WithWallType> allWalls = NewFindWallsInDirections(floorPositions);

        // remove duplicates
        Vector2WithWallTypeComparer comparer = new Vector2WithWallTypeComparer();

        HashSet<Vector2WithWallType> uniqueWalls = new HashSet<Vector2WithWallType>(allWalls, comparer);

        terrainCreator.PaintWalls(uniqueWalls);
    }

    private static HashSet<Vector2WithWallType> NewFindWallsInDirections(HashSet<Vector2Int> floorPositions)
    {
        HashSet<Vector2WithWallType> allWalls = new();
        for (int i = 0; i < floorPositions.Count; i++)
        {
            Vector2Int floorPosition = floorPositions.ElementAt(i);

            for (int j = 0; j < Direction2D.eightDirectionsList.Count; j++)
            {
                Vector2Int possibleNewWallPosition = floorPosition + Direction2D.eightDirectionsList.ElementAt(j);
                if (floorPositions.Contains(possibleNewWallPosition) == false)  // don't create wall at floorTile
                {
                    HashSet<Vector2Int> tileCollisions = CheckNeighbourCollisions(floorPositions, possibleNewWallPosition);
                    WallType wallType = ConvertCollisionsToWallType(tileCollisions);
                    if (wallType != WallType.NONE)
                    {
                        allWalls.Add(new Vector2WithWallType(possibleNewWallPosition, wallType));
                    }
                }
            }
        }

        return allWalls;
    }

    private static HashSet<Vector2Int> CheckNeighbourCollisions(HashSet<Vector2Int> floorPositions, Vector2Int possibleWallPosition)
    {
        HashSet<Vector2Int> collisions = new HashSet<Vector2Int>();
        for (int i = 0; i < Direction2D.eightDirectionsList.Count; i++)
        {
            Vector2Int direction = Direction2D.eightDirectionsList.ElementAt(i);
            Vector2Int possibleFloorPosition = possibleWallPosition + direction;

            if (floorPositions.Contains(possibleFloorPosition) == true)
            {
                collisions.Add(direction);
            }
        }

        return collisions;
    }


    private static WallType ConvertCollisionsToWallType(HashSet<Vector2Int> collisions)
    {
        // Get all values of the WallType enum
        WallType[] wallTypesArray = (WallType[])Enum.GetValues(typeof(WallType));

        // Iterate through each enum value
        foreach (WallType wallType in wallTypesArray)
        {
            List<Vector2Int>[] allowedFloorCollisions = WallConstants.GetAllowedFloorCollisions(wallType);
            foreach (var item in allowedFloorCollisions)
            {
                if (collisions.SetEquals(item) == true) // Compares if both sets contain the exact same element
                {
                    return wallType;
                }
            }
        }

        // if no match was found, no wall should be there
        return WallType.NONE;
    }

}

