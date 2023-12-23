using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorMap, wallMap;

    [SerializeField]
    private TileBase tile;

    [SerializeField]
    private List<TileBase> walls;

    public void Clear()
    {
        floorMap.ClearAllTiles();
        wallMap.ClearAllTiles();

    }

    // IEnumberable: generic form of a collection
    public void PaintFloor(IEnumerable<Vector2Int> tilePositions)
    {
        foreach (var position in tilePositions)
        {
            PaintSingleTile(position, floorMap, tile);
        }
    }

    private TileBase GetRandomWall()
    {
        return walls[UnityEngine.Random.Range(0, walls.Count)];

    }

    public void PaintWalls(IEnumerable<Vector2Int> positions)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(position, wallMap, GetRandomWall());
        }
    }

    private void PaintSingleTile(Vector2Int position, Tilemap tileMap, TileBase tileType)
    {
        var tilePosition = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePosition, tileType);
    }
}
