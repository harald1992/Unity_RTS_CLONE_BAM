using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// abstract so it cannot be created
public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TerrainCreator terrainCreator = null;

    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField]
    protected List<GameObject> enemyPrefabs;

    [SerializeField]
    protected List<GameObject> objectPrefabs;


    public void GenerateDungeon()
    {
        ClearMap();
        RunProceduralGeneration();
    }

    public void ClearMap()
    {
        terrainCreator.Clear();
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
            bool spawnEnemy = Random.Range(0f, 1f) < 0.5f;
            if (amountOfEnemies < 2 && spawnEnemy)   // 10% change of a tile generating an enemy
            {
                Vector2Int spawnPosition = path.ElementAt(i);
                GameObject randomEnemyPrefab = enemyPrefabs.ElementAt(Random.Range(0, enemyPrefabs.Count));
                GameObject ob = ObjectInstantiator.instance.InstantiateEnemy(randomEnemyPrefab, spawnPosition);
                amountOfEnemies++;
            }
        }
    }

    protected void SpawnObjects(HashSet<Vector2Int> path)
    {
        for (int i = 0; i < path.Count; i++)    // skip i=0 because that is the player spawn room
        {
            bool spawnObject = Random.Range(0f, 1f) < 0.3f;
            if (spawnObject)   // 10% change of a tile generating an enemy
            {
                Vector2Int roomPosition = path.ElementAt(i);
                Vector3 spawnPosition = new Vector3(roomPosition.x, roomPosition.y, 0);

                GameObject randomObjectPrefab = objectPrefabs.ElementAt(Random.Range(0, objectPrefabs.Count));
                ObjectInstantiator.instance.InstantiateObject(randomObjectPrefab, spawnPosition);
            }
        }
    }

}
