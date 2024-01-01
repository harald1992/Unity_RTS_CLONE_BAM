using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class WallGenerator : MonoBehaviour
{

    // public static void CreateWalls(HashSet<Vector2Int> floorPositions, TerrainCreator terrainCreator)
    public void CreateWalls()

    {

        TerrainCreator terrainCreator = GameObject.FindWithTag("TerrainCreator").GetComponent<TerrainCreator>();
        terrainCreator.Clear();
        HashSet<Vector2Int> floorPositions = new()
        {
        new()
        };



        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                floorPositions.Add(new Vector2Int(x, y));
            }
        }

        floorPositions.Add(new Vector2Int(2, 3));
        floorPositions.Add(new Vector2Int(2, 4));
        floorPositions.Add(new Vector2Int(2, 5));
        floorPositions.Add(new Vector2Int(2, 6));
        floorPositions.Add(new Vector2Int(2, 7));

        for (int x = 2; x < 5; x++)
        {
            for (int y = 7; y < 10; y++)
            {
                floorPositions.Add(new Vector2Int(x, y));
            }
        }

        floorPositions.Add(new Vector2Int(5, 8));
        floorPositions.Add(new Vector2Int(5, 9));
        floorPositions.Add(new Vector2Int(5, 10));

        for (int x = 5; x < 8; x++)
        {
            for (int y = 10; y < 13; y++)
            {
                floorPositions.Add(new Vector2Int(x, y));
            }
        }

        terrainCreator.PaintAllCorridors(floorPositions);

        HashSet<Vector2WithWallType> allWalls = NewFindWallsInDirections(floorPositions);

        // foreach (var item in allWalls)
        // {
        //     Debug.Log(item.ToString());
        // }


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
        Debug.Log("Check Neighbour Collisions in: " + possibleWallPosition);
        for (int i = 0; i < Direction2D.eightDirectionsList.Count; i++)
        {
            Vector2Int direction = Direction2D.eightDirectionsList.ElementAt(i);
            Vector2Int possibleFloorPosition = possibleWallPosition + direction;

            // Debug.Log("Possible Floor Position " + possibleFloorPosition);
            if (floorPositions.Contains(possibleFloorPosition) == true)
            {
                // Debug.Log("FloorPositions contains " + possibleFloorPosition);
                collisions.Add(direction);
            }

        }

        string collisions_string = "Collision: ";
        foreach (var item in collisions)
        {
            collisions_string += " " + item;
        }
        Debug.Log(collisions_string);

        return collisions;
    }


    private static WallType ConvertCollisionsToWallType(HashSet<Vector2Int> collisions)
    {
        bool isDone = false;
        // Get all values of the WallType enum
        WallType[] wallTypesArray = (WallType[])Enum.GetValues(typeof(WallType));

        // Iterate through each enum value
        foreach (WallType wallType in wallTypesArray)
        {
            if (isDone == true)
            {
                break;
            }
            List<Vector2Int>[] allowedFloorCollisions = WallConstants.GetAllowedFloorCollisions(wallType);
            foreach (var item in allowedFloorCollisions)
            {
                if (isDone == true)
                {
                    break;
                }
                bool areEqual = collisions.SetEquals(item); // Compares if both sets contain the exact same elements
                if (areEqual == true)
                {
                    isDone = true;
                    return wallType;
                }
            }
        }

        // if no match was found, no wall should be there
        return WallType.NONE;
    }

}

