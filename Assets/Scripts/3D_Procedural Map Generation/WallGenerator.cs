using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class WallGenerator : MonoBehaviour
{

    public void CreateWalls(HashSet<Vector3Int> floorPositions, TerrainCreator terrainCreator)
    {
        if (terrainCreator == null)
        {
            terrainCreator = GameObject.FindWithTag("TerrainCreator").GetComponent<TerrainCreator>();
            terrainCreator.PaintAllCorridors(floorPositions);
        }



        HashSet<Vector3WithWallType> allWalls = NewFindWallsInDirections(floorPositions);

        // remove duplicates
        Vector3WithWallTypeComparer comparer = new();

        HashSet<Vector3WithWallType> uniqueWalls = new(allWalls, comparer);

        terrainCreator.PaintWalls(uniqueWalls);
    }

    private static HashSet<Vector3WithWallType> NewFindWallsInDirections(HashSet<Vector3Int> floorPositions)
    {
        HashSet<Vector3WithWallType> allWalls = new();
        for (int i = 0; i < floorPositions.Count; i++)
        {
            Vector3Int floorPosition = floorPositions.ElementAt(i);

            for (int j = 0; j < Direction3D.eightDirectionsList.Count; j++)
            {
                Vector3Int possibleNewWallPosition = floorPosition + Direction3D.eightDirectionsList.ElementAt(j);
                if (floorPositions.Contains(possibleNewWallPosition) == false)  // don't create wall at floorTile
                {
                    HashSet<Vector3Int> tileCollisions = CheckNeighbourCollisions(floorPositions, possibleNewWallPosition);
                    WallType wallType = ConvertCollisionsToWallType(tileCollisions);
                    if (wallType != WallType.NONE)
                    {
                        allWalls.Add(new Vector3WithWallType(possibleNewWallPosition, wallType));
                    }
                }
            }
        }

        return allWalls;
    }

    private static HashSet<Vector3Int> CheckNeighbourCollisions(HashSet<Vector3Int> floorPositions, Vector3Int possibleWallPosition)
    {
        HashSet<Vector3Int> collisions = new();
        for (int i = 0; i < Direction3D.eightDirectionsList.Count; i++)
        {
            Vector3Int direction = Direction3D.eightDirectionsList.ElementAt(i);
            Vector3Int possibleFloorPosition = possibleWallPosition + direction;

            if (floorPositions.Contains(possibleFloorPosition) == true)
            {
                collisions.Add(direction);
            }
        }

        return collisions;
    }


    private static WallType ConvertCollisionsToWallType(HashSet<Vector3Int> collisions)
    {
        // Get all values of the WallType enum
        WallType[] wallTypesArray = (WallType[])Enum.GetValues(typeof(WallType));

        // Iterate through each enum value
        foreach (WallType wallType in wallTypesArray)
        {
            List<Vector3Int>[] allowedFloorCollisions = WallConstants.GetAllowedFloorCollisions(wallType);
            foreach (var item in allowedFloorCollisions)
            {
                if (collisions.SetEquals(item) == true) // Compares if both sets contain the exact same element
                {
                    return wallType;
                }
            }
        }
        Debug.Log("Wall none");

        // if no match was found, no wall should be there
        return WallType.NONE;
    }

}

