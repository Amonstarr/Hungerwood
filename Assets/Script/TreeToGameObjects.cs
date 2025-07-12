using UnityEngine;

public class TreeToGameObjects : MonoBehaviour
{
    public Vector2 areaSize = new Vector2(1000, 1000);
    public int treeCount = 5000;
    public GameObject[] treePrefabs;
    public Terrain terrain;
    
    [Header("Parent")]
    public Transform parent;                         // Tempat pohon ditaruh (biar rapi di Hierarchy)

    void Start()
    {
        GenerateTrees();
    }

    void GenerateTrees()
    {
        for (int i = 0; i < treeCount; i++)
        {
            GameObject selectedTree = treePrefabs[Random.Range(0, treePrefabs.Length)];

            float x = Random.Range(0, areaSize.x);
            float z = Random.Range(0, areaSize.y);
            float y = terrain.SampleHeight(new Vector3(x, 0, z));

            Vector3 worldPosition = new Vector3(x, y, z) + terrain.transform.position;

            GameObject tree = Instantiate(selectedTree, worldPosition, Quaternion.identity, parent);
            tree.name = "Tree_" + i;
        }
    }
}
