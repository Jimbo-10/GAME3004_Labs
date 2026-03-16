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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Regenerate();
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

                    }
                }
                
            }
        }
    }
}
