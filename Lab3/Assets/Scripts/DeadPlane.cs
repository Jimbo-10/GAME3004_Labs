using UnityEngine;

public class DeadPlane : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            other.transform.position = new Vector3(other.transform.position.x - Vector3.Normalize(other.transform.position).x,10,
                                                    other.transform.position.z - Vector3.Normalize(other.transform.position).z);
        }
    }
}
