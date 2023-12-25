// using System;
// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;

// public static class AStar
// {


//     public static List<Vector3> GetPath(Vector3 startPosition, Vector3 exit, bool[] obstacles)
//     {
//         VertexPosition startVertex = new VertexPosition(startPosition, false);
//         VertexPosition exitVertex = new VertexPosition(exit, false);

//         List<Vector3> path = new List<Vector3>();

//         List<VertexPosition> openedList = new List<VertexPosition>();   // nodes queued up for searching
//         HashSet<VertexPosition> closedList = new HashSet<VertexPosition>(); // nodes that already have been searched

//         startVertex.estimatedCost = ManhattanDistance(startVertex, exitVertex);
//         openedList.Add(startVertex);
//         VertexPosition currentVertex = null;

//         while (openedList.Count > 0)
//         {
//             openedList.Sort();
//             currentVertex = openedList[0];

//             if (currentVertex.Equals(exitVertex))   // if reached the destination, go back from exit to start.
//             {
//                 while (currentVertex != startVertex)
//                 {   // don't go back to startVertex
//                     path.Add(currentVertex.Position);
//                     currentVertex = currentVertex.previousVertex;
//                 }
//                 path.Reverse();
//                 break;
//             }
//             else
//             {
//                 GameObject mapGrid = null;
//                 var arrayOfNeighbours = FindNeighboursFor(currentVertex, mapGrid, obstacles)
//             }
//         }

//         return path;

//     }

//     // private static VertexPosition FindNeighboursFor(VertexPosition currentVertex, GameObject grid, bool[] obstacles)
//     // {
//     //     VertexPosition[] arrayOfNeighbours = new VertexPosition[4];
//     //     int arrayIndex = 0;

//     //     foreach (var possibleNeighbour in VertexPosition.possibleNeighbours)
//     //     {
//     //         // Vector3 position = new Vector3(currentVertex.X, +possibleNeighbour.Z)
//     //     }

//     // }

//     // sum of the x and y difference
//     private static float ManhattanDistance(VertexPosition startVertex, VertexPosition exitVertex)
//     {
//         return Mathf.Abs(startVertex.X - exitVertex.X) + Mathf.Abs(startVertex.Y - exitVertex.Y);
//     }




// }
