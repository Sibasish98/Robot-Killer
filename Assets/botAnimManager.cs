using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class botAnimManager : MonoBehaviour
{
    //Power up rotator
    // Start is called before the first frame update
    void Start()
    {
       // selfAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        rotateBase();
    }
    void rotateBase()
    {
        transform.Rotate(new Vector3(0, 100 * Time.deltaTime , 0));
    }
}
