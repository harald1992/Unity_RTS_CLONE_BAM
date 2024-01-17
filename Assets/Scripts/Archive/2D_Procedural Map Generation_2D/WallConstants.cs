// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Vector2WithWallTypeComparer : IEqualityComparer<Vector2WithWallType>
// {
//     public bool Equals(Vector2WithWallType v1, Vector2WithWallType v2)
//     {
//         return v1.position == v2.position; // Compare Vector2 positions for equality
//     }

//     public int GetHashCode(Vector2WithWallType v)
//     {
//         return v.position.GetHashCode(); // Get the hash code of the Vector2 position
//     }
// }

// public enum WallType
// {
//     UP,
//     UP_RIGHT,   // outer corner
//     UP_RIGHT_INNER,
//     RIGHT,
//     RIGHT_DOWN,
//     RIGHT_DOWN_INNER,
//     DOWN,
//     DOWN_LEFT,
//     DOWN_LEFT_INNER,

//     LEFT,
//     LEFT_UP,
//     LEFT_UP_INNER,
//     DEFAULT,
//     NONE,
// }

// public class Vector2WithWallType
// {
//     public Vector2 position;
//     public WallType wallType;

//     public Vector2WithWallType(Vector2 pos, WallType type)
//     {
//         position = pos;
//         wallType = type;
//     }
// }

// public static class WallConstants
// {
//     public static List<Vector2Int>[] GetAllowedFloorCollisions(WallType wallType)
//     {


//         List<Vector2Int>[] allowedFloorCollisions = wallType switch
//         {
//             WallType.UP => up,
//             WallType.UP_RIGHT => upRight,
//             WallType.UP_RIGHT_INNER => upRightInner,

//             WallType.RIGHT => right,
//             WallType.RIGHT_DOWN => rightDown,
//             WallType.RIGHT_DOWN_INNER => rightDownInner,

//             WallType.DOWN => down,
//             WallType.DOWN_LEFT => downLeft,
//             WallType.DOWN_LEFT_INNER => downLeftInner,

//             WallType.LEFT => left,
//             WallType.LEFT_UP => leftUp,
//             WallType.LEFT_UP_INNER => leftUpInner,


//             _ => empty,  // default
//         };
//         return allowedFloorCollisions;
//     }

//     public static readonly List<Vector2Int>[] empty = new List<Vector2Int>[]
//        {
//         new()
//        };

//     public static readonly List<Vector2Int>[] up = new List<Vector2Int>[]
//      {
//         new() { new Vector2Int(0, -1) },
//         new() { new Vector2Int(-1, -1), new Vector2Int(0, -1) },
//         new() { new Vector2Int(0, -1), new Vector2Int(1, -1) },
//         new() { new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, -1) },
//      };

//     public static readonly List<Vector2Int>[] upRight = new List<Vector2Int>[]
// {
//             new() { new Vector2Int(-1, -1) },
// };

//     public static readonly List<Vector2Int>[] upRightInner = new List<Vector2Int>[]
// {
// // the full
//         new() { new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, -1) },

// // full except top left one
//         new() {  new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, -1) },

// // full except rightbottom one
//         new() { new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, -1) },

// // only middle 3 
//         new() {  new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, -1)},
// };

//     public static readonly List<Vector2Int>[] right = new List<Vector2Int>[]
// {
//         new() { new Vector2Int(-1, 0) },
//         new() { new Vector2Int(-1, -1), new Vector2Int(-1, 0) },
//         new() { new Vector2Int(-1, 0), new Vector2Int(-1, 1) },
//         new() { new Vector2Int(-1, -1), new Vector2Int(-1, 0), new Vector2Int(-1, 1) },
// };

//     public static readonly List<Vector2Int>[] rightDown = new List<Vector2Int>[]
// {
//         new() { new Vector2Int(-1, 1) },
// };


//     public static readonly List<Vector2Int>[] rightDownInner = new List<Vector2Int>[]
// {
// // the full
//         new() { new Vector2Int(-1, -1), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1) },

// // full except bottom left one
//         new() { new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1) },

// // full except rightup one
//         new() { new Vector2Int(-1, -1), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, 1) },

// // middle 3
//         new() {  new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, 1) },

// };

//     public static readonly List<Vector2Int>[] down = new List<Vector2Int>[]
//     {
//         new() { new Vector2Int(0, 1) },
//         new() { new Vector2Int(-1, 1), new Vector2Int(0, 1) },
//         new() { new Vector2Int(0, 1), new Vector2Int(1, 1) },
//         new() { new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1) },
//     };

//     public static readonly List<Vector2Int>[] downLeft = new List<Vector2Int>[]
// {
//         new() { new Vector2Int(1, 1) },
// };

//     public static readonly List<Vector2Int>[] downLeftInner = new List<Vector2Int>[]
// {
// // the full
//         new() { new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, 1), new Vector2Int(-1, 1) },

// // full except bottom right one
//         new() {  new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, 1), new Vector2Int(-1, 1) },

// // full except left up one
//         new() { new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, 1)},

// // middle 3
//         new() {  new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, 1) },


// };

//     public static readonly List<Vector2Int>[] left = new List<Vector2Int>[]
// {
//         new() { new Vector2Int(1, 0) },
//         new() { new Vector2Int(1, 1), new Vector2Int(1, 0) },
//         new() { new Vector2Int(1, 0), new Vector2Int(1, -1) },
//         new() { new Vector2Int(1, 1), new Vector2Int(1, 0), new Vector2Int(1, -1) },
// };


//     public static readonly List<Vector2Int>[] leftUp = new List<Vector2Int>[]
// {
//         new() { new Vector2Int(1, -1) },
// };

//     public static readonly List<Vector2Int>[] leftUpInner = new List<Vector2Int>[]
// {
// // the full
//         new() { new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(1, 1) },

// // full except bottom left one
//         new() {  new Vector2Int(0, -1), new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(1, 1) },

// // full except left up one
//         new() { new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, -1), new Vector2Int(1, 0) },

// // middle 3
//         new() {  new Vector2Int(0, -1), new Vector2Int(1, -1), new Vector2Int(1, 0) },


// };


// }
