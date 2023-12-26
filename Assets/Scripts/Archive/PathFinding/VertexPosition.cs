// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UIElements;

// // store all information for the A*
// // Vertices in shapes are the points where two or more line segments or edges meet
// public class VertexPosition : IEquatable<VertexPosition>, IComparable<VertexPosition>
// {

//     public static List<Vector2Int> possibleNeighbours = new List<Vector2Int>(Direction2D.cardinalDirectionsList);

//     public float totalCost, estimatedCost;

//     public VertexPosition previousVertex = null;

//     private Vector3 position;

//     private bool isTaken;

//     public int X { get => (int)position.x; }

//     public int Y { get => (int)position.y; }

//     public bool IsTaken { get => isTaken; }

//     public Vector3 Position { get => position; }

//     public VertexPosition(Vector3 position, bool isTaken = false)
//     {
//         this.position = position;
//         this.isTaken = isTaken;
//         this.estimatedCost = 0;
//         this.totalCost = 1;
//     }

//     public int GetHashCOde(VertexPosition obj)
//     {
//         return obj.GetHashCode();
//     }

//     public override int GetHashCode()
//     {
//         return position.GetHashCode();
//     }

//     public int CompareTo(VertexPosition other)
//     {
//         if (this.estimatedCost < other.estimatedCost)
//         {
//             return -1;
//         }

//         if (this.estimatedCost > other.estimatedCost)
//         {
//             return 1;
//         }

//         return 0;
//     }

//     public bool Equals(VertexPosition other)
//     {
//         return Position == other.Position;
//     }



// }
