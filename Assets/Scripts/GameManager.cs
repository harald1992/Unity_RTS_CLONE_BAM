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
    // public HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

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

        CreateNewDungeon();
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
    }

    public void CreateNewDungeon()
    {
        GameObject container = GameObject.FindWithTag("GameObjectContainer");
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject);
        }

        if (mapGenerator == null)
        {
            mapGenerator = transform.Find("CorridorFirstMapCreator").GetComponent<CorridorFirstMapCreator>();
        }

        mapGenerator.GenerateDungeon();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerStats.instance.CastSpell(0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerStats.instance.CastSpell(1);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerStats.instance.CastSpell(2);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerStats.instance.CastSpell(3);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerStats.instance.CastSpell(4);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
        }


    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("ON SCENE LOADED CALLED");
    }
}
