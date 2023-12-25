using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{

    public GameObject gameManagerPrefab;
    public GameObject playerPrefab;

    public GameObject mapGeneratorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnGameManager();
        SpawnMapGenerator();
        // SpawnPlayer();
    }

    private void SpawnMapGenerator()
    {
        GameObject mapGenerator = Instantiate(mapGeneratorPrefab);
        mapGenerator.transform.Find("CorridorFirstMapGenerator").GetComponent<CorridorFirstMapCreator>().GenerateDungeon();
    }

    private void SpawnEnemies()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void SpawnGameManager()
    {
        if (GameManager.instance == null)
        {
            GameObject gameManagerObject = Instantiate(gameManagerPrefab);
            GameManager.instance = gameManagerObject.GetComponent<GameManager>();
            DontDestroyOnLoad(gameManagerObject);  // dont destroy object when changing scene
        }
    }
}
