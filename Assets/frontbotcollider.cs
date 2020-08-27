using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frontbotcollider : MonoBehaviour
{
    public collidTest ct;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Front coolider");
        ct.frontColliderHiting = true;
        //ct.fireNow();
    }
    private void OnTriggerStay(Collider other)
    {
        ct.frontColliderHiting = true;
        //ct.fireNow();
        ct.lookAround = false;
        ct.fireNow();

    }
    private void OnTriggerExit(Collider other)
    {
        ct.frontColliderHiting = false;
        //ct.calculateBotRoateDirection();
        ct.lookAround = true;
        if (!ct.IsInvoking("disablelookaround"))
        {
            ct.calculateBotRoateDirection();
            ct.Invoke("disablelookaround", 4f);
        }
        else
        {
            ct.CancelInvoke("disablelookaround");
            ct.calculateBotRoateDirection();
            ct.Invoke("disablelookaround", 4f);
        }
    }
}
