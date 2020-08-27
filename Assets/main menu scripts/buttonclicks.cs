using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonclicks : MonoBehaviour
{
    public GameObject intermediateObject;
    public arenaSelector aselector;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onPlayButtonPressed()
    {
        GameObject yy = Instantiate(intermediateObject, Vector3.zero, Quaternion.identity);
        yy.GetComponent<intermediate>().choosedArena = aselector.currentSelectedArena;
        SceneManager.LoadScene(1);
    }
    public void onArenaButtonPressed(GameObject arenaPanel)
    {
        arenaPanel.SetActive(true);
    }
    public void onOkayButtonPressed(Button bb)
    {
        bb.transform.parent.gameObject.SetActive(false);
    }
}
