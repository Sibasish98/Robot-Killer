using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinManager : MonoBehaviour
{
    public int coins;
    public int unclockedArenas;
    // Start is called before the first frame update
    void Start()
    {
        readUserCoin();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void readUserCoin()
    {
        coins = 200;
    }
    public void write()
    {
        //write to storage about new coins
        ;
    }
    public void setCoins(int s)
    {
        coins = s;
    }
    public int getCoins()
    {
        return coins;
    }
    public void readUnclockedArenas()
    {
        unclockedArenas = 2;
    }
}
