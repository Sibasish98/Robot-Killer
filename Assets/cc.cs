using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.SceneManagement;
public class cc : MonoBehaviour
{
    public int currentScore;
    float scoreMultiplierTimer;
    float scoreMultiplierMAXTime = 7f;
    int normalCoinreward = 50;


    public bool sheildActivated = false;
    public bool rageActivated = false;
    public GameObject shieldHitEffect;
    public AudioSource shieldHit;

    public powerupmgr powerUpMgr;
    public AudioSource powerPickedUpAudio;

    public AudioSource collideSound, playerexplodeSound,botexplodeSound;

    //This is player's script ideally
    public GameObject playerdamageEffect;
    public GameObject botdamageEffect;
    public TMP_Text speedd;
    GameObject cm;
    public float speed = 50f;
    float turnSpeed = 80f;

    public float maxspeed = 60f;
    public float acceleration = 10f;
    public float minspeed = 20f;
    int halfscrwidth;
    bool hited = false;
    Vector3 hitDir;

    bool speedIncrease = true;
    Rigidbody rb;
    GameObject cam;
    bool lockSpeed = false;
    bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        cm = this.gameObject;
        cam = Camera.main.gameObject;
        halfscrwidth = Screen.width / 2;
        //cm = Camera.main.gameObject;
        rb = cm.GetComponent<Rigidbody>();
        powerUpMgr = GetComponent<powerupmgr>();
    }

    // Update is called once per frame
    void Update()
    {
        //handleInput();
        manageSpeed();
       // Debug.Log((int)speed);
       speedd.text = ((int)currentScore).ToString();    // debug text view on center of screen
       // Debug.Log(rb.velocity);

        //score multiplier timer
        if (scoreMultiplierTimer > 0f)
        {
            scoreMultiplierTimer -= Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        if (!gameOver)
        handleInputRigidBody();

        if (hited)
        {
            transform.Rotate(-hitDir * 280 * Time.deltaTime);
        }
    }
    void handleInputRigidBody()
    {
        //FOr keyboard inputs
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //rb.MovePosition(rb.gameObject.transform.position + new Vector3(0, 0, speed * Time.deltaTime));
            if(!lockSpeed)
            rb.MovePosition(rb.gameObject.transform.position + (rb.transform.forward * speed * Time.deltaTime));
            speedIncrease = false;
        }
        else
        {
            if (!lockSpeed)
                rb.MovePosition(rb.gameObject.transform.position + (rb.transform.forward * speed * Time.deltaTime));

            //rb.MovePosition(rb.gameObject.transform.position + new Vector3(0, 0, speed * Time.deltaTime));
            speedIncrease = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turnSpeed * Time.deltaTime, 0));
            //cam.transform.rotation = rb.rotation;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, -turnSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //speed *= 2;
            lockSpeed = true;
            //rb.AddForce(-rb.transform.forward * 400);
            rb.AddForce(new Vector3(0, 700f, -600f));
            //rb.MovePosition(rb.gameObject.transform.position - (rb.transform.forward * 100 * Time.deltaTime));
            Invoke("unlockspeed", 1.8f);
        }
        //For Android
        if (Input.touchCount >= 2)
        {
            if ((Input.GetTouch(0).position.x >= halfscrwidth && Input.GetTouch(1).position.x < halfscrwidth) || (Input.GetTouch(0).position.x < halfscrwidth && Input.GetTouch(1).position.x >= halfscrwidth))
            {
                //for down arrow replicate action
                //rb.MovePosition(rb.gameObject.transform.position + new Vector3(0, 0, speed * Time.deltaTime));
                if(!lockSpeed)
                 rb.MovePosition(rb.gameObject.transform.position + (rb.transform.forward * speed * Time.deltaTime));
                speedIncrease = false;
            }
            else
            {
                if (!lockSpeed)
                    rb.MovePosition(rb.gameObject.transform.position + (rb.transform.forward * speed * Time.deltaTime));

                //rb.MovePosition(rb.gameObject.transform.position + new Vector3(0, 0, speed * Time.deltaTime));
                speedIncrease = true;
            }
        }
        else
        {
            //FOr turning the player
            if (!Application.isEditor)
            {
                if (Input.GetTouch(0).position.x >= halfscrwidth)
                {
                    //for right tap
                    rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turnSpeed * Time.deltaTime, 0));
                    //cam.transform.rotation = rb.rotation;
                }
                else
                {
                    //for left tap
                    rb.MoveRotation(rb.rotation * Quaternion.Euler(0, -turnSpeed * Time.deltaTime, 0));
                }
            }
        }
    }
    void handleInput()
    {
        // if (Input.GetKey(KeyCode.UpArrow))
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cm.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            speedIncrease = false;
        }
        else
        {
            cm.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            speedIncrease = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cm.transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cm.transform.Rotate(new Vector3(0, -turnSpeed * Time.deltaTime, 0));
        }
        //For android 
        if (Input.touchCount >= 2)
        {
            if ((Input.GetTouch(0).position.x >= halfscrwidth && Input.GetTouch(1).position.x < halfscrwidth) || (Input.GetTouch(0).position.x < halfscrwidth && Input.GetTouch(1).position.x >= halfscrwidth))
            {
                //for down arrow
                cm.transform.Translate(new Vector3(0, 0, speed  * Time.deltaTime));
                speedIncrease = false;
            }
            else
            {
                cm.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
                speedIncrease = true;
            }
        }
        else
        {
            if (!Application.isEditor)
            {
                cm.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
                speedIncrease = true;
                if (Input.GetTouch(0).position.x >= halfscrwidth)
                {
                    cm.transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0));
                }
                else
                {
                    cm.transform.Rotate(new Vector3(0, -turnSpeed * Time.deltaTime, 0));
                }
            }
        }
    }
    void manageSpeed()
    {
        if (speedIncrease && speed <= maxspeed)
        {
            if (!lockSpeed)
            speed += acceleration * Time.deltaTime;
        }
        if (!speedIncrease && speed >= minspeed)
        {
            speed -= acceleration * Time.deltaTime;
        }
    }
    void unlockspeed()
    {
        lockSpeed = false;
        speed = 20f;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("cl "+other.gameObject.name);
        if (other.gameObject.tag == "powerup")
        {
            Debug.Log("Powerup");
            powerPickedUpAudio.Play();
            powerUpMgr.powerUpGrabed();
            powerUpMgr.CancelInvoke("powerupVanisher");
            Destroy(other.gameObject); //to destroy the powerup cube
            //powerUpMgr.disableLightPowerup();
            //powerUpMgr.activateShieldPowerup();
            //powerUpMgr.activateSpeedPowerup();
        }
       if (other.gameObject.tag == "bot")
        {
            //if player hits the bot
            if (!gameOver)
            {
                collidTest x = other.gameObject.GetComponentInChildren<collidTest>();
                if (!x.isDead)
                {
                    x.currentBot.updatePosition = false;
                    x.currentBot.updateRotation = false;
                    x.currentBot.updateUpAxis = false;
                    x.coneLight.SetActive(false);
                    x.rangeLight.SetActive(false);
                    x.GetComponent<BoxCollider>().enabled = false;
                    x.frontCollider.enabled = false;
                    //x.bodyCollider.enabled = false;
                    x.fwdPlr = transform.forward;
                    x.gotHit = true;
                    x.Invoke("disbalegotHit", 0.4f);
                    Instantiate(botdamageEffect, other.transform.position, Quaternion.identity);
                    x.isDead = true;
                    botexplodeSound.Play();

                    //score management code below
                    if (scoreMultiplierTimer <= 0f)
                    {
                        currentScore += normalCoinreward; //increment the score/coin or whatever
                        scoreMultiplierTimer = scoreMultiplierMAXTime;
                    }
                    else
                    {
                        currentScore += (2 * normalCoinreward);
                        //scoreMultiplierTimer = (scoreMultiplierMAXTime/2);
                    }
                }
                else
                    collideSound.Play();
                //(other.gameObject);

                if (!rageActivated) //powerup related boolean value
                {
                    //test
                    lockSpeed = true;
                    //rb.AddForce(-rb.transform.forward * 400);
                    Vector3 temp = rb.gameObject.transform.forward;
                    temp = temp * -700f;
                    temp.y = 700f;
                    // rb.AddForce(new Vector3(0, 700f, -700f)); //worling force
                    if (!gameOver)
                    {
                        rb.AddForce(temp);
                    }
                    //if (other.gameObject.tag != "bullet")
                    //rb.AddForce(temp);
                    //rb.MovePosition(rb.gameObject.transform.position - (rb.transform.forward * 100 * Time.deltaTime));
                    Invoke("unlockspeed", 2.8f);
                }
            }
        }
        else if (other.gameObject.tag == "envir")
        {
            lockSpeed = true;
            //rb.AddForce(-rb.transform.forward * 400);
            Vector3 temp = rb.gameObject.transform.forward;
            temp = temp * -700f;
            temp.y = 700f;
            // rb.AddForce(new Vector3(0, 700f, -700f)); //worling force
            if (other.GetType() == typeof(SphereCollider) || other.gameObject.transform.parent.name == "Boundaries")
            {
                collideSound.Play();
                rb.AddForce(temp);
            }
                //if (other.gameObject.tag != "bullet")
                //rb.AddForce(temp);
                //rb.MovePosition(rb.gameObject.transform.position - (rb.transform.forward * 100 * Time.deltaTime));
                Invoke("unlockspeed", 2.8f);
        }
    }
    private void OnParticleCollision(GameObject other)
    {

        if (!sheildActivated)
        {
            Debug.Log("Bullet hit");
            Instantiate(playerdamageEffect, other.transform.position, Quaternion.identity);
            //GetComponent<BoxCollider>().isTrigger = true; //make the player drown
            hitDir = other.transform.forward;
            playerexplodeSound.Play();
            gameOver = true;
            rb.detectCollisions = false;
            rb.isKinematic = true;
            hited = true;
            Invoke("disablehited", 0.5f);
        }
        else
        {
            shieldHit.Play();
            Vector3 tmpp = transform.position;
            tmpp.y = 9f;
            Destroy(Instantiate(shieldHitEffect, tmpp, Quaternion.identity), 1.5f);
        }
        
    }
    public void tempReset()
    {
        SceneManager.LoadScene(0);
    }
    void disablehited()
    {
        hited = false;
        rb.detectCollisions = true;
    }
}