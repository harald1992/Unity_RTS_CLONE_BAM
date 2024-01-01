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

            // Destroy(child.gameObject);
            Destroy(child.gameObject);
        }
    }

    public void PaintAllCorridors(IEnumerable<Vector2Int> tilePositions)
    {
        foreach (var position in tilePositions)
        {
            Vector3 cubePosition = new Vector3(position.x, position.y, 0);
            GameObject corridorTile = Instantiate(corridorPrefab, cubePosition, Quaternion.Euler(0f, 0f, 0f));
            // corridorTile.transform.parent = corridorContainer.transform;
            corridorTile.transform.parent = corridorContainer.transform;

        }
    }

    public void PaintWalls(HashSet<Vector2WithWallType> allWalls)
    {
        foreach (Vector2WithWallType vectorWithWallType in allWalls)
        {
            Vector3 wallPosition = vectorWithWallType.position;

            GameObject wallPrefab = ConvertWallTypeToPrefab(vectorWithWallType.wallType);
            Vector3 offSet = wallPrefab.transform.position;

            GameObject wall = Instantiate(wallPrefab, wallPosition + offSet, Quaternion.identity);
            wall.transform.parent = wallContainer.transform;


            // GameObject blackoverlay = wallPrefabs.FirstOrDefault(obj => obj.name == "WallBlackOverlay");
            // Vector3 overlayOffset = blackoverlay.transform.position;
            // GameObject blackoverlayObject = Instantiate(blackoverlay, wallPosition + overlayOffset, Quaternion.identity);
            // blackoverlayObject.transform.parent = wall.transform;
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


    public void PaintUniqueRoom(HashSet<Vector2Int> roomPositions)
    {
        foreach (var position in roomPositions)
        {
            Vector3 tilePosition = new Vector3(position.x, position.y, -0.1f);
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