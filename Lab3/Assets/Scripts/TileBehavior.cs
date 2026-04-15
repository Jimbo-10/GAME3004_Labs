using System.Collections.Generic;
using UnityEngine;
public enum TileType
{
    Grass,
    Dirt,
    Stone,
    Coal,
    Iron,
    Gold,
    Diamond
}

public class TileBehavior : MonoBehaviour
{
    public bool isActiveTile = true;
    public TileType type = TileType.Stone;

    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        UpdateTile();
    }

    List<GameObject> neighbourTiles = new List<GameObject>();

   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickAxe"))
        {
            if (isActiveTile == true)
            {
                //Activate the tile's neighbour which is visible now
                foreach (GameObject tile in neighbourTiles)
                {
                    if(tile != null && tile.GetComponent<TileBehavior>().isActiveTile == false)
                    {
                        tile.GetComponent<TileBehavior>().ActivateTile();
                    }
                }
            }

            Destroy(gameObject);
        }
    }

    public void InActiveTile()
    {
        isActiveTile = false;

        var boxCollider = transform.GetComponent<BoxCollider>();
        var meshRenderer = transform.GetComponent<MeshRenderer>();

        boxCollider.enabled = false;
        meshRenderer.enabled = false;
    }

    void UpdateTile()
    {
        switch (type)
        {
            case TileType.Grass:
                meshRenderer.material.color = Color.green;
                break;
            case TileType.Dirt:
                meshRenderer.material.color = new Color(0.545f, 0.271f, 0.075f);
                break;
            case TileType.Stone:
                meshRenderer.material.color = new Color(0.134f, 0.135f, 0.133f);
                break;
            case TileType.Coal:
                meshRenderer.material.color = new Color(0.052f, 0.056f, 0.044f);
                break;
            case TileType.Iron:
                meshRenderer.material.color = new Color(0.115f, 0.128f, 0.158f);
                break;
            case TileType.Gold:
                meshRenderer.material.color = Color.yellow;
                break;
            case TileType.Diamond:
                meshRenderer.material.color = Color.cyan;
                break;
        }
    }

    public void ActivateTile()
    {
        isActiveTile = true;

        var boxCollider = transform.GetComponent<BoxCollider>();
        var meshRenderer = transform.GetComponent<MeshRenderer>();

        boxCollider.enabled = true;
        meshRenderer.enabled = true;

        type = (TileType)Random.Range(0, System.Enum.GetValues(typeof(TileType)).Length);
        UpdateTile();
    }

    public void AddNeighbour(GameObject tile)
    {
        neighbourTiles.Add(tile);
    }
}
