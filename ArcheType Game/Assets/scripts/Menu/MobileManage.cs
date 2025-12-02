using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobileManage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mobilePainel;
    public bool dev = false;
    public UnityEvent mobileEvents;
    void Start()
    {
        mobilePainel.SetActive(false);

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || dev)
        {
            mobilePainel.SetActive(true);
            Debug.Log("Mobile interface enabled.");
            mobileEvents.Invoke();
        }
        else
        {
            mobilePainel.SetActive(false);
            Debug.Log("Not a mobile platform, mobile interface disabled.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
