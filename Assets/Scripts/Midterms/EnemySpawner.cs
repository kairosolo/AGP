using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject terrainObject;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private int treeCount = 15;
    [SerializeField] private float minTreeDistance = 3f;
    [SerializeField] private Vector2 terrainSize;
    [SerializeField] private int currentEnemies;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        int spawnedTrees = 0; int attempts = 0;
        while (spawnedTrees < treeCount && attempts < 300)
        {
            attempts++;
            Vector3 randomPosition = new Vector3(Random.Range(-terrainSize.x / 2f, terrainSize.x / 2f), 50f, Random.Range(-terrainSize.y / 2f, terrainSize.y / 2f));
            randomPosition += terrainObject.transform.position;
            if (Physics.Raycast(randomPosition, Vector3.down, out RaycastHit hit, 100f))
            {
                if (Physics.OverlapSphere(hit.point, minTreeDistance, enemyMask).Length > 0) continue;

                GameObject generatedTree = Instantiate(enemy, hit.point, Quaternion.identity);
                generatedTree.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                spawnedTrees++;
                currentEnemies++;
            }
        }
        // GameManager.Instance.SetNumberEnemies(currentEnemies);
    }

    public void DecreaseCurrentEnemies()
    {
        currentEnemies--;
        // GameManager.Instance.SetNumberEnemies(currentEnemies);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(terrainObject.transform.position, new Vector3(terrainSize.x, 1f, terrainSize.y));
    }
}