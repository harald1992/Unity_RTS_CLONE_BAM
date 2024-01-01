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
        terrainCreator.PaintAllCorridors(corridorPositions);

        // Create roomposition at every dead end, for if the mapwalk walked back the same direction.
        List<Vector2Int> allDeadEnds = FindAllDeadEnds(corridorPositions);
        roomPositions.UnionWith(allDeadEnds);

        CreateRooms(roomPositions);

        HashSet<Vector2Int> allWalkableTiles = new HashSet<Vector2Int>();
        allWalkableTiles.UnionWith(corridorPositions);
        allWalkableTiles.UnionWith(roomPositions);
        wallGenerator.CreateWalls(allWalkableTiles, terrainCreator);
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
            HashSet<Vector2Int> path;
            if (i == 0)
            {
                path = CreatePlayerRoom(roomPositions.ElementAt(i));
            }
            else if (i == roomPositions.Count - 1)
            {
                path = CreateExitRoom(roomPositions.ElementAt(i));
            }
            else
            {
                float roomSeed = Random.Range(0f, 1f);
                if (roomSeed < 0.3f)
                {
                    path = CreateLibrary(roomPositions.ElementAt(i));
                }
                else if (roomSeed > 0.3f && roomSeed < 0.6f)
                {
                    path = CreateTreasury(roomPositions.ElementAt(i));
                }
                else
                {
                    path = CreateStandardRoom(roomPositions.ElementAt(i));
                }
                SpawnEnemies(path);
            }

            newPositions.UnionWith(path);

        }


        roomPositions.UnionWith(newPositions);
    }

    private HashSet<Vector2Int> CreateExitRoom(Vector2Int position)
    {
        HashSet<Vector2Int> roomPath = CreateTorchRoom(position);
        terrainCreator.PaintUniqueRoom(roomPath);
        GameObject monolithEntrance = Resources.Load<GameObject>("Prefabs/Objects/Monolith_Entrance_0");
        ObjectInstantiator.instance.InstantiateObject(monolithEntrance, position);

        return roomPath;
    }

    private HashSet<Vector2Int> CreatePlayerRoom(Vector2Int position)
    {
        HashSet<Vector2Int> roomPath = CreateTorchRoom(position);
        terrainCreator.PaintUniqueRoom(roomPath);

        GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/Units/Knight");
        ObjectInstantiator.instance.InstantiatePlayer(playerPrefab, position);

        GameObject monolith = Resources.Load<GameObject>("Prefabs/Objects/Monolith_Exit");
        ObjectInstantiator.instance.InstantiateObject(monolith, position);

        return roomPath;
    }

    private HashSet<Vector2Int> CreateLibrary(Vector2Int position)
    {
        int xSize = 7;
        int ySize = 7;
        HashSet<Vector2Int> roomPath = ProceduralGenerationAlgorithms.CreateRoom(position, xSize, ySize);

        terrainCreator.PaintUniqueRoom(roomPath);
        GameObject rostrumPrefab = ObjectInstantiator.instance.GetPrefabByName("Rostrum_With_Book_Parent");
        ObjectInstantiator.instance.InstantiateObject(rostrumPrefab, position);

        return roomPath;
    }

    private HashSet<Vector2Int> CreateTreasury(Vector2Int position)
    {
        int xSize = 5;
        int ySize = 5;
        HashSet<Vector2Int> roomPath = ProceduralGenerationAlgorithms.CreateRoom(position, xSize, ySize);

        terrainCreator.PaintUniqueRoom(roomPath);
        GameObject goldPrefab = ObjectInstantiator.instance.GetRandomGoldPrefab();

        foreach (var pos in roomPath)
        {
            if (Random.Range(0f, 1f) < 0.2f)
            {
                ObjectInstantiator.instance.InstantiateObject(goldPrefab, pos);
            }
        }
        return roomPath;
    }

    private HashSet<Vector2Int> CreateTorchRoom(Vector2Int position)
    {
        int xSize = 7;
        int ySize = 7;
        HashSet<Vector2Int> roomPath = ProceduralGenerationAlgorithms.CreateRoom(position, xSize, ySize);

        Vector2Int leftTop = roomPath.ElementAt(0);
        Vector2Int rightTop = roomPath.ElementAt(xSize - 1);
        Vector2Int leftBottom = roomPath.ElementAt(roomPath.Count - xSize);
        Vector2Int rightBottom = roomPath.ElementAt(roomPath.Count - 1);

        GameObject torchPrefab = ObjectInstantiator.instance.GetPrefabByName("Torch");
        ObjectInstantiator.instance.InstantiateObject(torchPrefab, leftTop);
        ObjectInstantiator.instance.InstantiateObject(torchPrefab, rightTop);
        ObjectInstantiator.instance.InstantiateObject(torchPrefab, leftBottom);
        ObjectInstantiator.instance.InstantiateObject(torchPrefab, rightBottom);
        return roomPath;
    }

    private HashSet<Vector2Int> CreateStandardRoom(Vector2Int position)
    {
        int xSize = Random.Range(5, 9);
        if (xSize % 2 == 0)
        {
            xSize++;
        }
        int ySize = Random.Range(5, 9);
        if (ySize % 2 == 0)
        {
            ySize++;
        }
        HashSet<Vector2Int> path = ProceduralGenerationAlgorithms.CreateRoom(position, xSize, ySize);

        terrainCreator.PaintUniqueRoom(path);

        return path;
    }

}
