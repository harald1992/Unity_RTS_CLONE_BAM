using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject floatingTextPrefab;

    private CorridorFirstMapCreator mapGenerator;

    public static GameManager instance;
    public HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

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

        mapGenerator = transform.Find("CorridorFirstMapCreator").GetComponent<CorridorFirstMapCreator>();

        mapGenerator.GenerateDungeon();
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ObjectInstantiator.instance.InstantiateFloatingTextAt("30", GetMousePosition2D(), Color.white);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject knightPrefab = Resources.Load<GameObject>("Prefabs/Units/Knight");
            ObjectInstantiator.instance.InstantiatePlayer(knightPrefab, GetMousePosition2D());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject knightPrefab = Resources.Load<GameObject>("Prefabs/Units/Skeleton");
            ObjectInstantiator.instance.InstantiateEnemy(knightPrefab, GetMousePosition2D());
        }
    }

    private Vector2 GetMousePosition2D()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z; // Adjusting the z coordinate
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePosition;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("ON SCENE LOADED CALLED");
    }
}
