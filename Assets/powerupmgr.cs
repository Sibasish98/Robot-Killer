using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class powerupmgr : MonoBehaviour
{
    //spawnn
    public List<Texture> powerUpTextures;
    public List<float> powerUpTimmer;
    public Material powerUpCubeMat;
   

    public GameObject powerupcube;

    public Light mainDirectionalLight;
    public botManager botmgr;
    public Material underWaterr;
    // Start is called before the first frame update

    bool tornadoActivate = false;

    public Image slideBack, slideProg;
    //particle effect handler
    public GameObject shieldd;
    cc playerScript;
    public ParticleSystem powerPickedUpEffect, effect1, effect2;
    public GameObject rageEffect;
    public AudioSource powerupremove;
    public AudioSource sheildON;
    public AudioSource powerAvailable, powerRemoved;
    public Slider powerupRemaing;
    public GameObject powerPaneel;
    float timerr = 0f;

    float nextPowerUpTimeGap = 14f;
    float nextPowerUp = 0f;
    float vanishPowerUpTimer = 10f;
    float nextPowerUpTimer = 0f;
    float currentPowerTime = 0f;
    int powerNumber = -1;
    GameObject powerTempHolder;

    void Start()
    {
        playerScript = GetComponent<cc>();
        //if (botmgr.maxPatrolPoints != -1)
          //  spawnPowerUp();
    }
    // Update is called once per frame
    void Update()
    {
        //update slider if power up is activated
        if (timerr >= 0f)
        {
            timerr -= Time.deltaTime;
            powerupRemaing.value = timerr;
            Color a = slideBack.color;
            Color b = slideProg.color;
            a.a = timerr / 7f;
            b.a = timerr / 7f;
            slideBack.color = a;
            slideProg.color = b;
        } 
        //update next power up timmer
        if (nextPowerUpTimer > 0)
        {
            nextPowerUpTimer -= Time.deltaTime;
        }
        else
        {
            //time to spawn a power up
           
                spawnPowerUp();
        }
    }
    public void powerUpGrabed()
    {
        if (powerNumber == 0) disableLightPowerup();
        else if (powerNumber == 1) activateShieldPowerup();
        else if (powerNumber == 2) activateSpeedPowerup();

        nextPowerUpTimer = currentPowerTime + nextPowerUpTimeGap;
    }
    public void spawnPowerUp()
    {
        powerNumber = Random.Range(0, powerUpTextures.Count);
        powerUpCubeMat.mainTexture = powerUpTextures[powerNumber];
        //GameObject x = Instantiate(powerupcube, new Vector3(-100, 8, 40), Quaternion.identity);
        Vector3 temp = botmgr.patrolPoints[Random.Range(0, botmgr.maxPatrolPoints-1)].position;
        temp.y = 8f;
        powerTempHolder = Instantiate(powerupcube,temp , Quaternion.identity);
        powerAvailable.Play();
        currentPowerTime = powerUpTimmer[powerNumber];
        Invoke("powerupVanisher", vanishPowerUpTimer);
        nextPowerUpTimer = nextPowerUpTimeGap;
    }
    public void disableLightPowerup()
    {
        powerPickedUpEffect.Play();
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        List<NavMeshAgent> t = botmgr.bots;
        foreach (NavMeshAgent n in t)
        {
            n.gameObject.GetComponent<botattholder>().coneLight.SetActive(false);
            n.gameObject.GetComponent<botattholder>().collideTesttt.canFire = false;
            n.gameObject.GetComponent<botattholder>().sptLight.SetActive(false);
        }
        mainDirectionalLight.enabled = false;
        powerPaneel.SetActive(true);
        powerupRemaing.maxValue = 7f;
        powerupRemaing.value = 7f;
        timerr = 7f;
        Invoke("removedisableLightPowerup", 7f);
    }
    void removedisableLightPowerup()
    {
        List<NavMeshAgent> t = botmgr.bots;
        foreach (NavMeshAgent n in t)
        {
            botattholder tp = n.gameObject.GetComponent<botattholder>();
            if (!tp.collideTesttt.isDead)
            {
                tp.coneLight.SetActive(true);
                tp.collideTesttt.canFire = true;
                n.gameObject.GetComponent<botattholder>().sptLight.SetActive(true);
            }
        }
        mainDirectionalLight.enabled = true;
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        powerupremove.Play();
        powerPaneel.SetActive(false);
    }
     public void activateShieldPowerup()
    {
        var mn = powerPickedUpEffect.main;
        mn.startColor = Color.cyan;
        mn = effect1.main;
        mn.startColor = Color.cyan;
        mn = effect2.main;
        mn.startColor = Color.cyan;
        powerPickedUpEffect.Play();





        Vector3 p = transform.position;
        p.y = 6f;
        GameObject sd = Instantiate(shieldd, p, transform.rotation);
        sd.transform.SetParent(this.transform);
        Destroy(sd,7f);
        //Destroy(sd, 3f);
        playerScript.sheildActivated = true;
        powerPaneel.SetActive(true);
        powerupRemaing.maxValue = 7f;
        powerupRemaing.value = 7f;
        timerr = 7f;
       Invoke("removeShieldPowerup", 7f);
    }
    void removeShieldPowerup()
    {
        powerPaneel.SetActive(false);
        playerScript.sheildActivated = false;
        powerupremove.Play();
    }
    public void activateSpeedPowerup()
    {
        var mn = powerPickedUpEffect.main;
        mn.startColor = Color.white;
        mn = effect1.main;
        mn.startColor = Color.white;
        mn = effect2.main;
        mn.startColor = Color.white;
        powerPickedUpEffect.Play();

        powerPaneel.SetActive(true);
        powerupRemaing.maxValue = 7f;
        powerupRemaing.value = 7f;
        timerr = 7f;

        
        GameObject ss = Instantiate(rageEffect, transform.position, rageEffect.transform.rotation);
        ss.transform.SetParent(transform);
        Destroy(ss, 7f);


        playerScript.maxspeed = 100f;
        playerScript.acceleration = 30f;
        playerScript.minspeed = 40f;
        playerScript.rageActivated = true;
        playerScript.sheildActivated = true;
        transform.localScale = new Vector3(4.1f, 4.1f, 4.1f);
        Invoke("removeRagePowerup", 7f);
    }
    public void removeRagePowerup()
    {

        transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        playerScript.acceleration = 10f;
        playerScript.maxspeed = 60f;
        playerScript.minspeed = 20f;
        playerScript.speed = 59f;
        playerScript.rageActivated = false;
        playerScript.sheildActivated = false;
    }
    
    public void powerupVanisher()
    {
        powerRemoved.Play();
        Destroy(powerTempHolder);
    }
}
