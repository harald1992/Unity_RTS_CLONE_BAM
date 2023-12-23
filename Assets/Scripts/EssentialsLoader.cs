using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{

    public GameObject gameManagerPrefab;
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnGameManager();
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnPlayer()
    {
        if (Player.instance == null)
        {
            GameObject playerObject = Instantiate(playerPrefab);
            playerObject.AddComponent<Player>();
            Player.instance = playerObject.GetComponent<Player>();
            DontDestroyOnLoad(playerObject);  // dont destroy object when changing scene
        }

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
