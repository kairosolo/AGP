using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> treeList;
    [SerializeField] private List<GameObject> generatedTreesList;
    [SerializeField] private GameObject terrainObject;
    [SerializeField] private LayerMask waterMask;
    [SerializeField] private LayerMask treeMask;
    [SerializeField] private int treeCount = 15;
    [SerializeField] private float minTreeDistance = 3f;
    [SerializeField] private Vector2 terrainSize;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateTrees();
        }
    }

    private void GenerateTrees()
    {
        if (generatedTreesList.Count > 0)
        {
            foreach (GameObject tree in generatedTreesList)
            {
                Destroy(tree);
            }
            generatedTreesList.Clear();
        }

        int spawnedTrees = 0; int attempts = 0;
        while (spawnedTrees < treeCount && attempts < 300)
        {
            attempts++;
            Vector3 randomPosition = new Vector3(Random.Range(-terrainSize.x / 2f, terrainSize.x / 2f), 50f, Random.Range(-terrainSize.y / 2f, terrainSize.y / 2f));
            randomPosition += terrainObject.transform.position;
            if (Physics.Raycast(randomPosition, Vector3.down, out RaycastHit hit, 100f))
            {
                if (((1 << hit.collider.gameObject.layer) & waterMask) != 0) continue;
                if (Physics.OverlapSphere(hit.point, minTreeDistance, treeMask).Length > 0) continue;

                GameObject generatedTree = Instantiate(treeList[Random.Range(0, treeList.Count - 1)], hit.point, Quaternion.identity);
                generatedTree.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                generatedTreesList.Add(generatedTree);
                spawnedTrees++;
            }
        }
    }
}