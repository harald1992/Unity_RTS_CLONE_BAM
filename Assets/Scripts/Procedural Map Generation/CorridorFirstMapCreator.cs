using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstMapCreator : AbstractDungeonGenerator
{
    public int corridorAmount = 5, corridorLength = 10;

    [Range(0.1f, 1)]
    public float roomPercent = 0.3f;

    protected override void RunProceduralGeneration()
    {
        // ClearMap();
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

        CorridorMapWalk(floorPositions, roomPositions);

        List<Vector2Int> allDeadEnds = FindAllDeadEnds(floorPositions);
        roomPositions.UnionWith(allDeadEnds);

        CreateRooms(roomPositions);

        floorPositions.UnionWith(roomPositions);
        GameManager.instance.floorPositions = floorPositions;

        tilemapVisualizer.PaintFloor(floorPositions);

        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                {
                    neighboursCount++;
                }
            }

            if (neighboursCount == 1)
            {
                deadEnds.Add(position);
            }
        }

        return deadEnds;
    }

    protected void CorridorMapWalk(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> roomPositions)
    {
        var currentPosition = startPosition;
        floorPositions.Add(currentPosition);
        roomPositions.Add(currentPosition);

        for (int i = 0; i < corridorAmount; i++)
        {
            HashSet<Vector2Int> path = ProceduralGenerationAlgorithms.WalkRandomCorridor(currentPosition, corridorLength);
            currentPosition = path.ElementAt(path.Count - 1);

            bool isCreateRoom = Random.Range(0f, 1f) < roomPercent;
            if (isCreateRoom)
            {
                roomPositions.Add(currentPosition);
            }

            floorPositions.UnionWith(path);

        }
    }

    private void CreateRooms(HashSet<Vector2Int> roomPositions)
    {
        HashSet<Vector2Int> newPositions = new HashSet<Vector2Int>();

        foreach (var position in roomPositions)
        {

            HashSet<Vector2Int> path = CreateRoom(position);
            newPositions.UnionWith(path);


        }

        roomPositions.UnionWith(newPositions);
    }

    private HashSet<Vector2Int> CreateRoom(Vector2Int position)
    {
        HashSet<Vector2Int> path = ProceduralGenerationAlgorithms.CreateRoom(position);
        if (path.Contains(new Vector2Int(0, 0)))
        {
            SpawnPlayer(path.ElementAt(0));
        }
        else
        {
            SpawnEnemies(path);
            SpawnObjects(path);

        }
        return path;
    }




}
