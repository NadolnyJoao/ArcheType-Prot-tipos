using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiaMoviment : MonoBehaviour
{
    private Vector3 pos;
    public float speed;
    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, pos);
        if (dist > 1)
        {
            Vector3 dir = pos - transform.position;
            dir.y = 0;
            dir = dir.normalized;
            transform.Translate(dir * speed * Time.deltaTime);
            dist = Vector3.Distance(transform.position, pos);
            if (dist < 0.1f)
            {

                transform.position = pos;
            }
        }

    }

    public void SetPosition(Transform trans)
    {
        pos = trans.position;
        Debug.Log("axe");
    }
}
