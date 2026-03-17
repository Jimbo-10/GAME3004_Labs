using UnityEngine;

public class TileBehavior : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickAxe"))
        {
            Destroy(gameObject);
        }
    }
}
