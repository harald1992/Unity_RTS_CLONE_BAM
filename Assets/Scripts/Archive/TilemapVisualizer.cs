// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using Unity.Mathematics;
// using UnityEngine;
// using UnityEngine.Tilemaps;

// public class TilemapVisualizer : MonoBehaviour
// {
//     // [SerializeField]
//     // private Tilemap corridorMap, roomMap, wallMap;

//     // [SerializeField]
//     // private List<TileBase> corridors;

//     // private TileBase chosenCorridor;

//     // [SerializeField]
//     // private List<TileBase> rooms;

//     // [SerializeField]
//     // private List<TileBase> rooms3by3;

//     // [SerializeField]
//     // private List<TileBase> walls;

//     // private TileBase chosenWall;

//     [SerializeField]
//     private GameObject wallCube;

//     [SerializeField]
//     private GameObject corridorTile;

//     [SerializeField]
//     private GameObject roomTile;

//     public void Clear()
//     {
//         // corridorMap.ClearAllTiles();
//         // roomMap.ClearAllTiles();
//         // wallMap.ClearAllTiles();
//     }

//     public void PaintAllCorridors(IEnumerable<Vector2Int> tilePositions)
//     {
//         foreach (var position in tilePositions)
//         {
//             Vector3 cubePosition = new Vector3(position.x, position.y, 0);

//             GameObject cube = Instantiate(corridorTile, cubePosition, Quaternion.Euler(0f, 0f, 0f));

//         }
//     }

//     public void PaintAllWalls(IEnumerable<Vector2Int> positions)
//     {
//         foreach (var position in positions)
//         {
//             Vector3 cubePosition = new Vector3(position.x, position.y, 0);
//             // Vector3 position
//             GameObject cube = Instantiate(wallCube, cubePosition, Quaternion.identity);


//         }
//     }

//     // private void PaintSingleTile(Vector2Int position, Tilemap tileMap, TileBase tileType)
//     // {
//     //     var tilePosition = tileMap.WorldToCell((Vector3Int)position);
//     //     tileMap.SetTile(tilePosition, tileType);
//     // }

//     public void PaintUniqueRoom(HashSet<Vector2Int> roomPositions)
//     {

//         // if (roomPositions.Count == 9)
//         // {
//         //     chosenRoom = rooms3by3.ElementAt(UnityEngine.Random.Range(0, rooms3by3.Count));
//         //     PaintSingleTile(roomPositions.ElementAt(4), roomMap, chosenRoom);
//         // }
//         // else
//         // {
//         foreach (var position in roomPositions)
//         {
//             // PaintSingleTile(position, roomMap, chosenRoom);
//             Vector3 cubePosition = new Vector3(position.x, position.y, -0.1f);

//             GameObject cube = Instantiate(roomTile, cubePosition, Quaternion.Euler(0f, 0f, 0f));
//         }
//         // }


//     }



// }
