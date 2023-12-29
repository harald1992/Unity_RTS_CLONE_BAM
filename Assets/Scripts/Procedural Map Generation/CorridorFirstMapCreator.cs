using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstMapCreator : AbstractDungeonGenerator
{
    public int corridorAmount = 5, corridorLength = 10, roomSize = 3;

    [Range(0.1f, 1)]
    public float roomPercent = 0.3f;

    protected override void RunProceduralGeneration()
    {
        ClearMap();
        HashSet<Vector2Int> corridorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

        // create corridors and add the ends to roompositions
        CorridorMapWalk(corridorPositions, roomPositions);
        // already paint the corridors so the roompositions can overlay it afterwards
        tilemapVisualizer.PaintAllCorridors(corridorPositions);

        // Create roomposition at every dead end, for if the mapwalk walked back the same direction.
        List<Vector2Int> allDeadEnds = FindAllDeadEnds(corridorPositions);
        roomPositions.UnionWith(allDeadEnds);

        CreateRooms(roomPositions);

        // GameManager.instance.floorPositions = floorPositions;


        // tilemapVisualizer.PaintRooms(roomPositions);

        //
        HashSet<Vector2Int> allWalkableTiles = new HashSet<Vector2Int>();
        allWalkableTiles.UnionWith(corridorPositions);
        allWalkableTiles.UnionWith(roomPositions);
        WallGenerator.CreateWalls(allWalkableTiles, tilemapVisualizer);
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> corridorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (var position in corridorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (corridorPositions.Contains(position + direction))
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

        for (int i = 0; i < roomPositions.Count; i++)
        {
            if (i == 0)
            {

                HashSet<Vector2Int> path = CreateRoom(roomPositions.ElementAt(i));
                newPositions.UnionWith(path);

                GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/Units/Knight");
                ObjectInstantiator.instance.InstantiatePlayer(playerPrefab, roomPositions.ElementAt(i));

                GameObject monolith = Resources.Load<GameObject>("Prefabs/Objects/Monolith_Exit");
                ObjectInstantiator.instance.InstantiateObject(monolith, roomPositions.ElementAt(i));

            }
            else if (i == roomPositions.Count - 1)
            {
                GameObject monolithEntrance = Resources.Load<GameObject>("Prefabs/Objects/Monolith_Entrance_0");
                ObjectInstantiator.instance.InstantiateObject(monolithEntrance, roomPositions.ElementAt(i));

                HashSet<Vector2Int> path = CreateRoom(roomPositions.ElementAt(i));
                newPositions.UnionWith(path);
                SpawnEnemies(path);
                SpawnObjects(path);
            }
            else
            {
                HashSet<Vector2Int> path = CreateRoom(roomPositions.ElementAt(i));
                newPositions.UnionWith(path);
                SpawnEnemies(path);
                SpawnObjects(path);
            }


        }


        roomPositions.UnionWith(newPositions);
    }

    private HashSet<Vector2Int> CreateRoom(Vector2Int position)
    {


        int xMax = Random.Range(1, 5);
        int yMax = Random.Range(1, 5);
        HashSet<Vector2Int> path = ProceduralGenerationAlgorithms.CreateRoom(position, xMax, yMax);
        // HashSet<Vector2Int> path = ProceduralGenerationAlgorithms.SimpleRandomWalk(position, 30);

        // Paint this room
        tilemapVisualizer.PaintUniqueRoom(path);
        // tilemapVisualizer.PaintCorridors(path);

        return path;
    }




}
