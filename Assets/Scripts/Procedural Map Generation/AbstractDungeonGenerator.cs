using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// abstract so it cannot be created
public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;

    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField]
    protected List<GameObject> enemyPrefabs;

    public void GenerateDungeon()
    {
        ClearMap();
        RunProceduralGeneration();
    }

    public void ClearMap()
    {
        tilemapVisualizer.Clear();
        ClearEnemies();
    }

    private void ClearEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    protected abstract void RunProceduralGeneration();

    protected void SpawnEnemies(HashSet<Vector2Int> path)
    {
        int amountOfEnemies = 0;
        for (int i = 0; i < path.Count; i++)    // skip i=0 because that is the player spawn room
        {
            float range = Random.Range(0f, 1f);
            if (amountOfEnemies < 2 && range < 0.1f)   // 10% change of a tile generating an enemy
            {
                Vector2Int roomMiddlePosition = path.ElementAt(i);
                Vector3 spawnPosition = new Vector3(roomMiddlePosition.x, roomMiddlePosition.y, 0);

                GameObject randomEnemyPrefab = enemyPrefabs.ElementAt(Random.Range(0, enemyPrefabs.Count));
                float yOffset = randomEnemyPrefab.GetComponent<CircleCollider2D>().offset.y;
                spawnPosition.y -= yOffset;

                GameObject playerObject = Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
                playerObject.tag = "Enemy";
                playerObject.AddComponent<Enemy>();
                amountOfEnemies++;
            }
        }
    }


    protected void SpawnPlayer()
    {
        if (Player.instance == null)
        {
            GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/Units/Skeleton");
            GameObject playerObject = Instantiate(playerPrefab);
            playerObject.AddComponent<Player>();

            playerObject.transform.position = new Vector3(0, 0, 0);
            Player.instance = playerObject.GetComponent<Player>();
            DontDestroyOnLoad(playerObject);  // dont destroy object when changing scene

            GameObject heroGate = Resources.Load<GameObject>("Prefabs/Objects/Hero_Gate");
            heroGate.transform.position = playerObject.transform.position;
            Instantiate(heroGate);
        }
    }

}
