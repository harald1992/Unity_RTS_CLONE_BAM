// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;

// public class SimpleRandomWalkMapGenerator : AbstractDungeonGenerator
// {
//     public int iterations = 30, walkLength = 30;
//     public bool startRandomlyEachIteration = true;

//     protected override void RunProceduralGeneration()
//     {
//         HashSet<Vector2Int> floorPositions = RunRandomWalk();
//         tilemapVisualizer.PaintFloor(floorPositions);

//         WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
//     }

//     protected HashSet<Vector2Int> RunRandomWalk()
//     {
//         HashSet<Vector2Int> floorPositions = ProceduralGenerationAlgorithms.RunRandomWalk(startPosition, iterations, walkLength, true);
//         return floorPositions;
//     }


// }
