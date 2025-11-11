using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManage : MonoBehaviour
{

    public GameObject popUpPrefab;

    public Transform canvasPai;

    public static NotificationManage instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("já existe um instance");
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    public void CreateNotification(Sprite img, string titletext, string bodytext)
    {

        var popUp = Instantiate(popUpPrefab, Vector3.zero, Quaternion.identity,canvasPai);
        Notification popNotificatino = popUp.GetComponent<Notification>();
        popNotificatino.SetData(img, titletext, bodytext);
        popNotificatino.StartAnimation();
        Destroy(popUp, 2);

    }
}
