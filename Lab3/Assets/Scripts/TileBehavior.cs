using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    bool isActiveTile = true;

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

    public void ActivateTile()
    {
        isActiveTile = true;

        var boxCollider = transform.GetComponent<BoxCollider>();
        var meshRenderer = transform.GetComponent<MeshRenderer>();

        boxCollider.enabled = true;
        meshRenderer.enabled = true;
    }

    public void AddNeighbour(GameObject tile)
    {
        neighbourTiles.Add(tile);
    }
}
