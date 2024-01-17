using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainCreator : MonoBehaviour
{
    public GameObject corridorContainer;
    public GameObject roomContainer;
    public GameObject wallContainer;


    public GameObject corridorPrefab;
    public GameObject roomPrefab;
    public List<GameObject> wallPrefabs;


    public void Clear()
    {
        DestroyAllChilds(corridorContainer);
        DestroyAllChilds(roomContainer);
        DestroyAllChilds(wallContainer);
    }

    private void DestroyAllChilds(GameObject ob)
    {
        for (int i = 0; i < ob.transform.childCount; i++)
        {
            Transform child = ob.transform.GetChild(i);

            Destroy(child.gameObject);
        }
    }

    public void PaintAllCorridors(IEnumerable<Vector3Int> tilePositions)
    {
        foreach (var position in tilePositions)
        {
            GameObject corridorTile = Instantiate(corridorPrefab, position, Quaternion.identity);
            corridorTile.transform.parent = corridorContainer.transform;

        }
    }

    public void PaintWalls(HashSet<Vector3WithWallType> allWalls)
    {
        foreach (Vector3WithWallType vectorWithWallType in allWalls)
        {
            Vector3 wallPosition = vectorWithWallType.position;

            GameObject wallPrefab = ConvertWallTypeToPrefab(vectorWithWallType.wallType);
            Vector3 offSet = wallPrefab.transform.position;

            GameObject wall = Instantiate(wallPrefab, wallPosition + offSet, Quaternion.identity);

            // GameObject wall = Instantiate(wallPrefab, wallPosition + offSet, Quaternion.Euler(90f, 0, 0));
            wall.transform.parent = wallContainer.transform;
        }
    }

    private GameObject ConvertWallTypeToPrefab(WallType wallType)
    {
        string wallName = wallType switch
        {
            WallType.UP => "UpWall",
            WallType.UP_RIGHT => "UpRightOuterWall",
            WallType.UP_RIGHT_INNER => "UpRightInnerWall",
            WallType.DOWN => "DownWall",
            WallType.DOWN_LEFT => "DownLeftOuterWall",
            WallType.DOWN_LEFT_INNER => "DownLeftInnerWall",
            WallType.RIGHT => "RightWall",
            WallType.RIGHT_DOWN => "RightDownOuterWall",
            WallType.RIGHT_DOWN_INNER => "RightDownInnerWall",
            WallType.LEFT => "LeftWall",
            WallType.LEFT_UP => "LeftUpOuterWall",
            WallType.LEFT_UP_INNER => "LeftUpInnerWall",

            _ => "DefaultWall",
        };

        GameObject wallPrefab = wallPrefabs.FirstOrDefault(obj => obj.name == wallName);
        if (wallPrefab == null)
        {
            wallPrefab = wallPrefabs.FirstOrDefault(obj => obj.name == "DefaultWall");
            Debug.Log("Wall prefab not found!");
        }
        return wallPrefab;
    }


    public void PaintUniqueRoom(HashSet<Vector3Int> roomPositions)
    {
        foreach (var position in roomPositions)
        {
            Vector3 tilePosition = new(position.x, 0.1f, position.z);
            GameObject roomTile = Instantiate(roomPrefab, tilePosition, Quaternion.identity);
            roomTile.transform.parent = roomContainer.transform;
        }
    }



    private void Update()
    {
        // if (Player.instance == null)
        // {
        //     return;
        // }
        // Vector3 playerPosition = Player.instance.gameObject.transform.position;

        // for (int i = 0; i < wallContainer.transform.childCount; i++)
        // {
        //     Transform child = wallContainer.transform.GetChild(i);
        //     if (child.position.y > playerPosition.y)
        //     {
        //         child.gameObject.layer = 6; // terrain, so sprite is overlaying
        //     }
        //     else
        //     {
        //         child.gameObject.layer = 0; // default so wall is over sprite
        //     }
        // }

    }

}