using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class intermediate : MonoBehaviour
{
    public int choosedArena;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("inter") == null)
            Destroy(this);
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += onSceneChanged;
    }
    void onSceneChanged(Scene s1,Scene s2)
    {
        if (s2.buildIndex == 1)
        {
            GameObject z = GameObject.FindGameObjectWithTag("loader");
            z.GetComponent<loaderr>().currentArena = choosedArena;
            z.GetComponent<loaderr>().setEnviroment();
            //Destroy(gameObject);
        }
        //any thing intermediate to game to menu do here
        else if (s2.buildIndex == 0)
        {
            //Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
