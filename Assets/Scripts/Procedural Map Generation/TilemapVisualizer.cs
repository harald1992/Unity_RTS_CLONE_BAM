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
    private List<TileBase> tiles;

    private TileBase chosenTile;

    [SerializeField]
    private List<TileBase> walls;

    private TileBase chosenWall;

    public void Clear()
    {
        floorMap.ClearAllTiles();
        wallMap.ClearAllTiles();
    }

    // IEnumberable: generic form of a collection
    public void PaintFloor(IEnumerable<Vector2Int> tilePositions)
    {
        SetRandomFloor();
        foreach (var position in tilePositions)
        {
            PaintSingleTile(position, floorMap, chosenTile);
        }
    }

    private void SetRandomFloor()
    {
        chosenTile = tiles[UnityEngine.Random.Range(0, walls.Count)];
    }

    private void SetRandomWall()
    {
        chosenWall = walls[UnityEngine.Random.Range(0, walls.Count)];
    }




    public void PaintWalls(IEnumerable<Vector2Int> positions)
    {
        SetRandomWall();
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
}
