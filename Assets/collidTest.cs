using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class collidTest : MonoBehaviour
{
    public AudioSource bulletLaunchSound;

    public BoxCollider frontCollider;
    public CapsuleCollider bodyCollider;
    public bool gotHit = false;
    public GameObject coneLight, rangeLight;
    public bool isDead = false;

    public NavMeshAgent currentBot;
    public bool triggerChase = false;
    public GameObject plr;
    public GameObject bullet, gunPoint;
    public Vector3 fwdPlr;

    public float cooldownTimeLimit = 1f;
    public bool canFire = true;
    public bool lookAround = false;
    public int dir = 0;
    public bool frontColliderHiting;
    // Start is called before the first frame update
    void Start()
    {
        //calculateBotRoateDirection();
        plr = GameObject.FindGameObjectWithTag("Player");
        dir = -1; //direction of rotation
    }
    // Update is called once per frame
    void Update()
    {
        /*if (triggerChase)
        {
            Vector3 te = plr.transform.position;
            te.y = 2;
            currentBot.SetDestination(plr.transform.position);
            
           // currentBot.transform.LookAt(plr.transform.position);
            if (!IsInvoking("disableChase"))
                Invoke("disableChase", 7f);
        }*/
        //   Debug.DrawRay(transform.position, -currentBot.transform.forward, Color.red,150f);
            if (gotHit)
            {
                fwdPlr.x = 0;
                fwdPlr.y = 0;
                Debug.Log(" tessst " + transform.parent.name);
                transform.parent.Rotate(fwdPlr * 280 * Time.deltaTime);
            }
            else if (lookAround)
                botlookaround();
        
        
    }
    void disableChase()
    {
        triggerChase = false;
        Debug.Log("Disbaling chase player");
    }
    private void OnTriggerEnter(Collider other)
    {
        //plr = other.gameObject;
        //for bullet fiure from bot
        /*currentBot.updateRotation = false;
        currentBot.transform.LookAt(other.gameObject.transform.position);
        GameObject a = Instantiate(bullet, gunPoint.transform.transform.position, Quaternion.identity);
        a.GetComponent<bulletMovement>().forwardDirection = gunPoint.transform;
        Destroy(a, 5f);
        currentBot.isStopped = true;
        lookAround = true;
        Invoke("disablelookaround",5f);
        Debug.Log("Side coolider");*/
        //Debug.Log(currentBot.transform.position.x - other.transform.position.x);
    }
    private void OnTriggerStay(Collider other)
    {
        /*if (other.gameObject.tag == "Player")
        {
            Debug.Log("Collided");
            plr = other.gameObject;
            currentBot.speed = 400;
            if (!triggerChase)
                triggerChase = true;
            else
                CancelInvoke("disableChase");*/
        //GameObject a = Instantiate(bullet, gunPoint.transform.transform.position, Quaternion.identity);
        //a.GetComponent<bulletMovement>().forwardDirection = gunPoint.transform;
        //Destroy(a, 5f);
        //}*/

        //cool down logic (prevents rapid fire)
        if (!frontColliderHiting)
        {
            lookAround = true;
            if (!IsInvoking("disablelookaround"))
            {
                calculateBotRoateDirection();
                Invoke("disablelookaround", 4f);
            }
            else
            {
                CancelInvoke("disablelookaround");
                calculateBotRoateDirection();
                Invoke("disablelookaround", 4f);
            }
        }
        else
        {
            ;// fireNow();
        }
        //raycast for checkung enemy in front
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (!frontColliderHiting)
        {
            //if bot does not detect player anymore in either collider
            //rotate around and get normal patroling
            ;
        }
    }
    public void botlookaround()
    {
        currentBot.isStopped = true;
        currentBot.updateRotation = false;
        //calculateBotRoateDirection();
        currentBot.transform.Rotate(new Vector3(0f, dir * 90f * Time.deltaTime, 0f));
    }
    public void disablelookaround()
    {
        lookAround = false;
        currentBot.isStopped = false;
        currentBot.updateRotation = true;
    }
    public void fireNow()
    {
        if (canFire) //&& frontColliderHiting)
        {
            canFire = false;
            currentBot.updateRotation = false; //disabled the AI auto rotation
            currentBot.isStopped = true; //disbale the AI auto movement
            bulletLaunchSound.Play();
            GameObject a = Instantiate(bullet, gunPoint.transform.transform.position, Quaternion.identity);
            a.GetComponent<bulletMovement>().forwardDirection = gunPoint.transform; //assign the foraward direction of gun show that the bullet propagates in right direction
            //Destroy(a, 5f);
            Invoke("enableFire", cooldownTimeLimit);
        }
    }
    public void enableFire()
    {
        canFire = true;
    }
    public void calculateBotRoateDirection()
    {
        dir = currentBot.transform.position.x - plr.transform.position.x > 0 ? 1 : -1;
    }
    public void disbalegotHit()
    {
        gotHit = false;
    }
}
