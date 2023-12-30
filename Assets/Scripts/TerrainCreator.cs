using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCreator : MonoBehaviour
{
    public GameObject corridorContainer;
    public GameObject roomContainer;
    public GameObject wallContainer;


    public GameObject corridorPrefab;
    public GameObject roomPrefab;
    public GameObject wallPrefab;


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

    public void PaintAllCorridors(IEnumerable<Vector2Int> tilePositions)
    {
        foreach (var position in tilePositions)
        {
            Vector3 cubePosition = new Vector3(position.x, position.y, 0);
            GameObject corridorTile = Instantiate(corridorPrefab, cubePosition, Quaternion.Euler(0f, 0f, 0f));
            corridorTile.transform.parent = corridorContainer.transform;
        }
    }

    public void PaintAllWalls(IEnumerable<Vector2Int> positions)
    {
        foreach (var position in positions)
        {
            Vector3 wallPosition = new Vector3(position.x, position.y, 0);
            GameObject wall = Instantiate(wallPrefab, wallPosition, Quaternion.identity);
            wall.transform.parent = wallContainer.transform;
        }
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
        if (Player.instance == null)
        {
            return;
        }
        float playerYPosition = Player.instance.gameObject.transform.position.y;

        for (int i = 0; i < wallContainer.transform.childCount; i++)
        {
            Transform child = wallContainer.transform.GetChild(i);
            if (child.position.y > playerYPosition)
            {
                child.gameObject.layer = 6; // terrain
            }
            else
                child.gameObject.layer = 0; // default

        }

    }

}