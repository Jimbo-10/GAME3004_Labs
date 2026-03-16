using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("World Properties")]
    [Range(8, 64)]
    public int height = 8;
    [Range(8, 64)]
    public int width = 8;
    [Range(8, 64)]
    public int depth = 8;

    [Header("Scaling Values")]
    [Range(8, 64)]
    public float min = 16.0f;
    public float max = 24.0f;

    [Header("Tile Properties")]
    public GameObject threeDTilePrefab;
    public Transform tileParent;

    List<GameObject> grid = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Regenerate();
        DisableColliderAndMeshRenderers();
    }

    private void Regenerate()
    {
        float randomScale = Random.Range(min, max);
        float offsetX = Random.Range(-1024.0f, 1024.0f);
        float offsetZ = Random.Range(-1024.0f, 1024.0f);

        for (int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++) 
                {
                    var perlinNoiseValue = Mathf.PerlinNoise((x + offsetX) / randomScale, (z + offsetZ) / randomScale) * depth * 0.5f;

                    if(y < perlinNoiseValue)
                    {
                        var tile = Instantiate(threeDTilePrefab, new Vector3(x,y,z), Quaternion.identity);
                        tile.transform.SetParent(tileParent);
                        grid.Add(tile);
                    }
                }
                
            }
        }
    }

    private void DisableColliderAndMeshRenderers()
    {
        // detect if each tile has Contacts with each face around

        var normalArray = new Vector3[] {Vector3.up, Vector3.down, Vector3.forward, Vector3.back, Vector3.right, Vector3.left };
        List<GameObject> disabledTiles = new List<GameObject>();

        foreach(GameObject tile in grid)
        {
            int collisionCounter = 0;
            for(int i = 0;  i < normalArray.Length; i++)
            {
                if(Physics.Raycast(tile.transform.position, normalArray[i], tile.transform.localScale.magnitude * 0.5f))
                {
                    collisionCounter++;
                }
            }
            if(collisionCounter > 5)
            {
                disabledTiles.Add(tile);
            }
        }

        foreach(GameObject tile in disabledTiles)
        {
            var boxCollider = tile.GetComponent<BoxCollider>();
            var meshRenderer = tile.GetComponent<MeshRenderer>();

            boxCollider.enabled = false;
            meshRenderer.enabled = true;
        }
    }
}
