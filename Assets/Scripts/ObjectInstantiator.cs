using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInstantiator : MonoBehaviour
{
    public static ObjectInstantiator instance;
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

    private GameObject InstantiateUnit(GameObject objectPrefab, Vector2 position)
    {
        GameObject ob = Instantiate(objectPrefab);
        Vector3 newPosition = new Vector3(position.x, position.y, 0);

        // compensate for collider, so object spawns with the collider in the middle of the tile.
        CircleCollider2D collider = ob.GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            Vector3 totalOffset = CalculateOffsetPlusRadius(collider.offset.x, collider.offset.y, collider.radius);
            newPosition -= totalOffset;
        }
        ob.transform.position = newPosition;
        return ob;
    }

    public GameObject InstantiatePlayer(GameObject objectPrefab, Vector2 position)
    {
        GameObject ob = InstantiateUnit(objectPrefab, position);
        ob.AddComponent<Player>();
        return ob;
    }

    public GameObject InstantiateEnemy(GameObject objectPrefab, Vector2 position)
    {
        GameObject ob = InstantiateUnit(objectPrefab, position);
        ob.AddComponent<Enemy>();
        return ob;
    }

    public GameObject InstantiateObject(GameObject objectPrefab, Vector2 position)
    {
        GameObject ob = Instantiate(objectPrefab);
        ob.transform.position = new Vector3(position.x, position.y, 0);
        return ob;
    }


    public void InstantiateFloatingTextAt(string text, Vector2 position)
    {
        GameObject floatingTextPrefab = Resources.Load<GameObject>("Prefabs/UI/FloatingText");

        position = new Vector3(position.x, position.y, 0);
        GameObject ob = Instantiate(floatingTextPrefab, position, Quaternion.identity);
        TextMesh mesh = ob.GetComponent<TextMesh>();
        mesh.text = text;
    }


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
}
