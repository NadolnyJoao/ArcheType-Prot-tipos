using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiaMoviment : MonoBehaviour
{
    private Vector3 pos;
    [Header("ir ate o oquadro")]
    public float minDist;
    public float speed;
    [Header("perto do player")]

    public Transform transfomrPlayer;
    void Start()
    {
        pos = transform.position;
        
    }

    void Update()
    {
        float distOfPicture = Vector3.Distance(transform.position, pos);
        float distOfPlayer = Vector3.Distance(transform.position, transfomrPlayer.position);
        if (distOfPicture > minDist)
        {
            Vector3 dir = pos - transform.position;
            dir.y = 0;
            dir = dir.normalized;
            transform.Translate(dir * speed * Time.deltaTime);
            distOfPicture = Vector3.Distance(transform.position, pos);
            if (distOfPicture < 0.1f)
            {

                transform.position = pos;
            }
        }

        if (distOfPlayer > 10)
        {
            pos = transfomrPlayer.position;
        }




    }

    public void SetPosition(Transform trans)
    {
        pos = trans.position;
        pos.y = transform.position.y;
        Debug.Log("axe");
    }
}
