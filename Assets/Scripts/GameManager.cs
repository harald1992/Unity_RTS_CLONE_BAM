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
    private GameObject currentSelectedItem;
    public HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

    // private AStarAlgorithm aStarAlgorithm = new AStarAlgorithm();


    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ObjectInstantiator.instance.SpawnFloatingTextAt("30", GetMousePosition2D());
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

        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Vector2 mousePosition = GetMousePosition2D();
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                // Debug.Log("Hit Object: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    currentSelectedItem = hit.collider.gameObject;
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        { // Right mouse button
            if (currentSelectedItem == null) { return; }

            Unit unitScript = currentSelectedItem.GetComponent<Unit>();
            if (unitScript != null)
            {
                // unitScript.MoveToDestination(GetMousePosition3D());

                HashSet<Vector2> walkPath = new()
                {
                    new Vector2(0,1),
                    new Vector2(1,1),
                    new Vector2(1,2),
                    new Vector2(2,2),
                    new Vector2(1,2),

                };

                AStarAlgorithm aStar = new AStarAlgorithm();

                walkPath = aStar.GetPath(new Vector2Int(0, 0), new Vector2Int(5, 5));

                for (int i = 0; i < walkPath.Count; i++)
                {
                    Debug.Log(walkPath.ElementAt(i));
                }
                // foreach (var item in walkPath)
                // {
                // Debug.Log(item.)
                //     Debug.Log(item);

                // }
                unitScript.MoveToDestinationInSteps(walkPath);
            }
        }
    }


    private Vector2 GetMousePosition2D()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z; // Adjusting the z coordinate
        // mousePos.z = 0;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePosition;
    }

    private Vector3 GetMousePosition3D()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z; // Adjusting the z coordinate
        // mousePos.z = 0;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePosition;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("ON SCENE LOADED CALLED");
    }
}
