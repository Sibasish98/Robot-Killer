using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class loaderr : MonoBehaviour
{
    //MIsclaenous
    public powerupmgr pg;
    public int currentArena;

    public GameObject boundryRocks;
    public GameObject gameManager;
    public Material underWater;
    public Material daySky, nightSky;
    public Material mc;// object material
    public Texture Jungle, Desert, Snow; //Texture data for shore element
    public GameObject snowfall;
    public GameObject jungleObjects, desertObjects, snowObjects; //decorative super game object of all theme based decorative items
    // Start is called before the first frame update
    void Start()
    {
        //setEnviroment();
        //spawn the game manager which will setup the bots automatically
        GameObject gamemgr = Instantiate(gameManager, new Vector3(0, 0, 0), Quaternion.identity);
        gamemgr.GetComponent<botManager>().numberOfBots = 10; //setting the numer of bots to spawn
        pg.botmgr = gamemgr.GetComponent<botManager>();
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Camera.main.activeTexture);
    }
    public void setEnviroment()
    {
        //Instantiate(boundryRocks, new Vector3(268, 93.5f, -259), Quaternion.identity);//create the boundary rocks
        int levelIndex = (int)Random.Range(0, 4);
        if (currentArena == 0) //1 stands for jungle level
        {
            RenderSettings.skybox = daySky;
            RenderSettings.ambientIntensity = 0.3f;
            underWater.color = new Color(0.05f, 0.43f + 5, 0.64f);
            mc.mainTexture = Jungle;
            Instantiate(jungleObjects, new Vector3(259, 84, -280), Quaternion.identity);
        }
        else if (currentArena == 1) //2 stands for desert level
        {
            RenderSettings.skybox = daySky;
            RenderSettings.ambientIntensity = 0.8f;
            underWater.color = new Color(0.62f, 0.48f + 5, 0f);
            mc.mainTexture = Desert;
            Instantiate(desertObjects, new Vector3(447, 35, 118), Quaternion.identity);
        }
        else if (currentArena == 2) //3 stands for snow level
        {
            RenderSettings.skybox = daySky;
            RenderSettings.ambientIntensity = 1f;
            //Instantiate(snowfall, new Vector3(0f, 0f, 0f), Quaternion.identity);
            underWater.color = new Color(0f, 2.5f, 5.4f);
            mc.mainTexture = Snow;
            Instantiate(snowObjects, new Vector3(59.77f, -22.73f, -135.874f), Quaternion.identity);
             Instantiate(snowfall, new Vector3(123f, 28.5f, 43f), Quaternion.identity);
        }
        RenderSettings.ambientIntensity = 0f;
    }
}
