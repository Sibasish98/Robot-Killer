using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class botManager : MonoBehaviour
{
    public GameObject mainBot;
    public int numberOfBots = 4;
    GameObject mainPatrolPoint;
    public List<NavMeshAgent> bots;
    public List<Transform> patrolPoints;
    public int maxPatrolPoints = -1;
    void Start()
    {
        //mainPatrolPoint = GameObject.FindGameObjectWithTag("patrols");
        bots = new List<NavMeshAgent>();
        patrolPoints = new List<Transform>();
        //checks all bot components in te main parent bot game object
        /*bots.AddRange(mainBot.GetComponentsInChildren<NavMeshAgent>());
        patrolPoints.AddRange(mainPatrolPoint.GetComponentsInChildren<Transform>());
        maxPatrolPoints = patrolPoints.Count;
        foreach (NavMeshAgent nv in bots)
        {
            assignDestination(nv);
        }*/
        //new logic instantite
        //populate patrol point lists
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag("patrolpoint"))
            patrolPoints.Add(temp.transform);
        maxPatrolPoints = patrolPoints.Count;
        //now instantiate bots
        for (int i=1;i <= numberOfBots;i++)
        {
            bots.Add(Instantiate(mainBot, patrolPoints[(int)Random.Range(0, maxPatrolPoints)].position, Quaternion.identity).GetComponent<NavMeshAgent>());
        }
    }
    void Update()
    {
        if (true)
        manageBotPatrol();   
    }
    void manageBotPatrol()
    {
         foreach (NavMeshAgent nv in bots)
         {
            if (nv != null && nv.enabled)
             if (nv.remainingDistance <= 2f)
             {
                 assignDestination(nv);
             }
         }
    }
    void assignDestination(NavMeshAgent nvv)
    {
        nvv.speed = 40f;
        int t = (int)Random.Range(0, maxPatrolPoints);
        nvv.SetDestination(patrolPoints[t].position);
    }
}
