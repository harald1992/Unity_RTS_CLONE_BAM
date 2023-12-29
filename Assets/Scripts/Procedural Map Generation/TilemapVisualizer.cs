using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap corridorMap, roomMap, wallMap;

    [SerializeField]
    private List<TileBase> corridors;

    private TileBase chosenCorridor;

    [SerializeField]
    private List<TileBase> rooms;

    [SerializeField]
    private List<TileBase> walls;

    private TileBase chosenWall;

    public void Clear()
    {
        corridorMap.ClearAllTiles();
        roomMap.ClearAllTiles();
        wallMap.ClearAllTiles();
    }

    public void PaintAllCorridors(IEnumerable<Vector2Int> tilePositions)
    {
        chosenCorridor = corridors[UnityEngine.Random.Range(0, corridors.Count)];
        foreach (var position in tilePositions)
        {
            PaintSingleTile(position, corridorMap, chosenCorridor);
        }
    }

    public void PaintAllWalls(IEnumerable<Vector2Int> positions)
    {
        chosenWall = walls[UnityEngine.Random.Range(0, walls.Count)];
        foreach (var position in positions)
        {
            PaintSingleTile(position, wallMap, chosenWall);
        }
    }

    private void PaintSingleTile(Vector2Int position, Tilemap tileMap, TileBase tileType)
    {
        var tilePosition = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePosition, tileType);
    }

    public void PaintUniqueRoom(HashSet<Vector2Int> roomPositions)
    {
        TileBase chosenRoom = rooms.ElementAt(UnityEngine.Random.Range(0, rooms.Count));

        foreach (var position in roomPositions)
        {
            PaintSingleTile(position, roomMap, chosenRoom);
        }
    }

}
