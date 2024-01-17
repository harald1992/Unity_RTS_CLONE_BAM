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
        HashSet<Vector3Int> corridorPositions = new();
        HashSet<Vector3Int> roomPositions = new();

        // create corridors and add the ends to roompositions
        CorridorMapWalk(corridorPositions, roomPositions);
        // already paint the corridors so the roompositions can overlay it afterwards
        terrainCreator.PaintAllCorridors(corridorPositions);

        // Create roomposition at every dead end, for if the mapwalk walked back the same direction.
        List<Vector3Int> allDeadEnds = FindAllDeadEnds(corridorPositions);
        roomPositions.UnionWith(allDeadEnds);

        CreateRooms(roomPositions);

        HashSet<Vector3Int> allWalkableTiles = new();
        allWalkableTiles.UnionWith(corridorPositions);
        allWalkableTiles.UnionWith(roomPositions);

        wallGenerator.CreateWalls(allWalkableTiles, terrainCreator);
    }

    private List<Vector3Int> FindAllDeadEnds(HashSet<Vector3Int> corridorPositions)
    {
        List<Vector3Int> deadEnds = new();

        foreach (var position in corridorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction3D.cardinalDirectionsList)
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

    protected void CorridorMapWalk(HashSet<Vector3Int> floorPositions, HashSet<Vector3Int> roomPositions)
    {
        var currentPosition = startPosition;
        floorPositions.Add(currentPosition);
        roomPositions.Add(currentPosition);
        Vector3Int lastDirection = Vector3Int.zero;

        for (int i = 0; i < corridorAmount; i++)
        {
            (HashSet<Vector3Int> path, Vector3Int _lastDirection) = ProceduralGenerationAlgorithms.WalkRandomCorridor(currentPosition, corridorLength, lastDirection);
            lastDirection = _lastDirection;
            currentPosition = path.ElementAt(path.Count - 1);

            bool isCreateRoom = Random.Range(0f, 1f) < roomPercent;
            if (isCreateRoom)
            {
                roomPositions.Add(currentPosition);
            }

            floorPositions.UnionWith(path);
        }
    }

    private void CreateRooms(HashSet<Vector3Int> roomPositions)
    {
        HashSet<Vector3Int> newPositions = new();

        for (int i = 0; i < roomPositions.Count; i++)
        {
            HashSet<Vector3Int> path;
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

    private HashSet<Vector3Int> CreateExitRoom(Vector3Int position)
    {
        HashSet<Vector3Int> roomPath = CreateTorchRoom(position, 5, 5);
        terrainCreator.PaintUniqueRoom(roomPath);
        GameObject monolithEntrance = Resources.Load<GameObject>("Prefabs/Objects/Monolith_Entrance_0");
        ObjectInstantiator.instance.InstantiateObject(monolithEntrance, position);

        return roomPath;
    }

    private HashSet<Vector3Int> CreatePlayerRoom(Vector3Int position)
    {
        HashSet<Vector3Int> roomPath = CreateTorchRoom(position, 5, 5);
        terrainCreator.PaintUniqueRoom(roomPath);

        // GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/Units/Skeleton");
        ObjectInstantiator.instance.InstantiatePlayer(playerPrefab, position);

        GameObject monolith = Resources.Load<GameObject>("Prefabs/Objects/Monolith_Exit");
        ObjectInstantiator.instance.InstantiateObject(monolith, position);

        return roomPath;
    }

    private HashSet<Vector3Int> CreateLibrary(Vector3Int position)
    {
        int xSize = 7;
        int zSize = 7;
        HashSet<Vector3Int> roomPath = CreateTorchRoom(position, xSize, zSize);

        terrainCreator.PaintUniqueRoom(roomPath);
        GameObject rostrumPrefab = ObjectInstantiator.instance.GetPrefabByName("Rostrum_With_Book_Parent");
        ObjectInstantiator.instance.InstantiateObject(rostrumPrefab, position);

        return roomPath;
    }

    private HashSet<Vector3Int> CreateTreasury(Vector3Int position)
    {
        int xSize = 7;
        int zSize = 7;
        HashSet<Vector3Int> roomPath = CreateTorchRoom(position, xSize, zSize);

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

    private HashSet<Vector3Int> CreateTorchRoom(Vector3Int position, int xSize, int zSize)
    {
        HashSet<Vector3Int> roomPath = ProceduralGenerationAlgorithms.CreateRoom(position, xSize, zSize);
        Vector3 leftTop = roomPath.ElementAt(0);
        Vector3 rightTop = roomPath.ElementAt(xSize - 1);
        Vector3 leftBottom = roomPath.ElementAt(roomPath.Count - xSize);
        Vector3 rightBottom = roomPath.ElementAt(roomPath.Count - 1);
        leftTop += new Vector3(0.5f, 0.15f, 0.5f);
        rightTop += new Vector3(-0.5f, 0.15f, 0.5f);
        leftBottom += new Vector3(0.5f, 0.15f, -0.5f);
        rightBottom += new Vector3(-0.5f, 0.15f, -0.5f);





        // GameObject torchPrefab = ObjectInstantiator.instance.GetPrefabByName("Candlestick_Floor_02_Lit");
        // ObjectInstantiator.instance.InstantiateObject(torchPrefab, leftTop);
        // ObjectInstantiator.instance.InstantiateObject(torchPrefab, rightTop);
        // ObjectInstantiator.instance.InstantiateObject(torchPrefab, leftBottom);
        // ObjectInstantiator.instance.InstantiateObject(torchPrefab, rightBottom);
        return roomPath;
    }

    private HashSet<Vector3Int> CreateStandardRoom(Vector3Int position)
    {
        int xSize = Random.Range(5, 9);
        if (xSize % 2 == 0)
        {
            xSize++;
        }
        int zSize = Random.Range(5, 9);
        if (zSize % 2 == 0)
        {
            zSize++;
        }
        HashSet<Vector3Int> path = CreateTorchRoom(position, xSize, zSize);

        terrainCreator.PaintUniqueRoom(path);

        return path;
    }

}
