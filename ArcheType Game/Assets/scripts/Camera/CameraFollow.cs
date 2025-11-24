using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public bool delay = false;
    public float speed = 1;

    [Header("Limits")]
    public List<Transform> limites = new List<Transform>();
    public float distLimit = 8;

    private void Start()
    {
        offset.z = -10;
    }
    private void Update()
    {
        transform.position = playerTransform.position + offset;

        foreach (var limite in limites)
        {
            Vector3 nposition = limite.position;
            nposition.z = transform.position.z;
            float dist = Vector3.Distance(transform.position, nposition);
           // Debug.Log("dist cam to limit " + dist);
            if (dist < distLimit)
            {
                if (limite.name.Contains("Right"))
                {
                    if (limite.position.x < transform.position.x)
                    {
                        // Vector3 direction = (transform.position - limite.position).normalized;
                        transform.position = new Vector3(limite.position.x, transform.position.y, transform.position.z);
                    }
                }
                else if (limite.position.x > transform.position.x)
                {
                    // Vector3 direction = (transform.position - limite.position).normalized;
                    transform.position = new Vector3(limite.position.x, transform.position.y, transform.position.z);
                }
            }
        }

    }
}
