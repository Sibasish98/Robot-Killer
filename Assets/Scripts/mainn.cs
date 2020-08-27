using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainn : MonoBehaviour
{
    public GameObject center, right, left;
    GameObject lastRoad;
    float lastzdirection = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //lastRoad = Instantiate(center, new Vector3(0f, 0f, 0f), Quaternion.identity);
        initialiseTest();
    }
    void initialiseTest()
    {
        for (int i=1;i<=4;i++)
        {
            int generateDirection = Random.Range(0, 3);
            Instantiate(center, new Vector3(0f, 0f, lastzdirection), Quaternion.identity);
            lastzdirection += 4.8f;
        }
        Instantiate(right, new Vector3(0f, 0f, lastzdirection-4.8f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
