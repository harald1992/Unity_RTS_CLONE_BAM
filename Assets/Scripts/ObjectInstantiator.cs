using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectInstantiator : MonoBehaviour
{
    public static ObjectInstantiator instance;

    public List<GameObject> gameObjectPrefabs;

    public List<GameObject> goldPrefabs;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private GameObject InstantiateUnit(GameObject objectPrefab, Vector3 position)
    {
        GameObject ob = Instantiate(objectPrefab);
        // Vector3 newPosition = new Vector3(position.x, position.y, 0);

        // compensate for collider, so object spawns with the collider in the middle of the tile.
        CircleCollider2D collider = ob.GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            Vector3 totalOffset = CalculateOffsetPlusRadius(collider.offset.x, collider.offset.y, collider.radius);
            position -= totalOffset;
        }
        ob.transform.position = position;

        // TODO: check if these are still needed in 3D
        if (ob.GetComponent<SpriteRenderer>() == true)
        {
            ob.transform.rotation = Quaternion.Euler(-30, -45, 60f);
        }

        return ob;
    }

    public GameObject InstantiatePlayer(GameObject objectPrefab, Vector3 position)
    {
        if (Player.instance != null)
        {
            Player.instance.transform.position = position;
            return Player.instance.gameObject;
        }
        else
        {
            GameObject ob = InstantiateUnit(objectPrefab, position);
            ob.AddComponent<Player>();
            ob.name = "Player";
            ob.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            return ob;
        }


    }

    public GameObject InstantiateEnemy(GameObject objectPrefab, Vector3 position)
    {
        GameObject ob = InstantiateUnit(objectPrefab, position);
        ob.AddComponent<Enemy>();
        GameObject container = GameObject.FindWithTag("GameObjectContainer");
        ob.transform.parent = container.transform;
        return ob;
    }

    public GameObject InstantiateObject(GameObject objectPrefab, Vector3 position)
    {
        GameObject ob = Instantiate(objectPrefab);

        // compensate for collider, so object spawns with the collider in the middle of the tile.
        CircleCollider2D collider = ob.GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            // TODO: check if still needed with 3D
            Vector3 totalOffset = CalculateOffsetPlusRadius(collider.offset.x, collider.offset.y, collider.radius);
            position -= totalOffset;
        }
        ob.transform.position = position;


        // TODO: check if these are still needed in 3D
        if (ob.GetComponent<SpriteRenderer>() == true)
        {
            ob.transform.rotation = Quaternion.Euler(-30, -45, 60f);
        }

        GameObject container = GameObject.FindWithTag("GameObjectContainer");
        ob.transform.parent = container.transform;
        return ob;
    }


    public void InstantiateFloatingTextAt(string text, Vector3 position, Color color)
    {
        GameObject floatingTextPrefab = Resources.Load<GameObject>("Prefabs/UI/FloatingText");

        position = new Vector3(position.x, position.y, 0);
        GameObject ob = Instantiate(floatingTextPrefab, position, Quaternion.identity);
        TextMesh mesh = ob.GetComponent<TextMesh>();
        mesh.text = text;
        mesh.color = color;
    }


    // TODO: check if still needed in 3D
    private Vector3 CalculateOffsetPlusRadius(float xOffset, float yOffset, float radius)
    {
        Vector3 totalOffset = Vector3.zero;

        if (xOffset != 0)
        {
            totalOffset.x = (Math.Abs(xOffset) + radius) * (xOffset / Math.Abs(xOffset));
        }

        if (yOffset != 0)
        {
            totalOffset.y = (Math.Abs(yOffset) + radius) * (yOffset / Math.Abs(yOffset));

        }
        return totalOffset;
    }

    public GameObject GetPrefabByName(string name)
    {
        foreach (var item in gameObjectPrefabs)
        {
            if (item.name.Contains(name))
            {
                return item;
            }
        }

        Debug.Log("Could not find prefab by name");
        return gameObjectPrefabs.ElementAt(0);

    }

    public GameObject GetRandomGoldPrefab()
    {
        return goldPrefabs.ElementAt(UnityEngine.Random.Range(0, goldPrefabs.Count));
    }

}
