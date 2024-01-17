// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;


// /*
//     Creates a generate button in all unity objects that have a class script with AbstractDungeonGenerator or extends that.
// */


// [CustomEditor(typeof(WallGenerator), true)]  // adds this editor to the abstractDungeonCreator class, so you get a button in the unity ui
// public class RandomDungeonGeneratorEditor : Editor
// {
//     WallGenerator generator;

//     private void Awake()
//     {
//         generator = (WallGenerator)target;
//     }

//     public override void OnInspectorGUI()
//     {
//         base.OnInspectorGUI();
//         if (GUILayout.Button("Create Dungeon"))
//         {
//             HashSet<Vector2Int> floorPositions = new()
//         {
//         new()
//         };

//             for (int x = 0; x < 5; x++)
//             {
//                 for (int y = 0; y < 4; y++)
//                 {
//                     floorPositions.Add(new Vector2Int(x, y));
//                 }
//             }

//             floorPositions.Add(new Vector2Int(2, 3));
//             floorPositions.Add(new Vector2Int(2, 4));
//             floorPositions.Add(new Vector2Int(2, 5));
//             floorPositions.Add(new Vector2Int(2, 6));
//             floorPositions.Add(new Vector2Int(2, 7));

//             for (int x = 2; x < 5; x++)
//             {
//                 for (int y = 7; y < 10; y++)
//                 {
//                     floorPositions.Add(new Vector2Int(x, y));
//                 }
//             }

//             floorPositions.Add(new Vector2Int(5, 8));
//             floorPositions.Add(new Vector2Int(5, 9));
//             floorPositions.Add(new Vector2Int(5, 10));

//             for (int x = 5; x < 8; x++)
//             {
//                 for (int y = 10; y < 13; y++)
//                 {
//                     floorPositions.Add(new Vector2Int(x, y));
//                 }
//             }
//             generator.CreateWalls(floorPositions, null);
//         }


//     }
// }
