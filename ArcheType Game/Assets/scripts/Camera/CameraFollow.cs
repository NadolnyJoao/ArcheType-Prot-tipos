using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public bool delay = false;
    public float speed = 1;

    private void Start()
    {
        offset.z = -10;
    }
    private void Update()
    {
        transform.position = playerTransform.position + offset;
    }
}
