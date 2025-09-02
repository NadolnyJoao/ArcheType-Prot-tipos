using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiaMoviment : MonoBehaviour
{
    private Vector3 pos;
    public Animator anima;
    [Header("ir ate o oquadro")]
    public float minDist;
    public float speed;
    [Header("perto do player")]

    public Transform transfomrPlayer;
    void Start()
    {
        pos = transform.position;
        anima = GetComponent<Animator>();
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
            anima.SetBool("Speed", (distOfPicture >0.1) );
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
