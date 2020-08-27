using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    //test
    public Transform forwardDirection;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("script is runing");
        Invoke("disablePS", 4f);
    }
    // Update is called once per frame
    void Update()
    {

        //if (forwardDirection != null)
        {
            Vector3 temp = forwardDirection.forward;
            //Vector3 temp = transform.forward;
            transform.Translate(temp * 50f * Time.deltaTime);
        }
    }
    void disablePS()
    {
        //GetComponent<ParticleSystem>().Stop();
        GameObject.Destroy(gameObject);
    }
}
