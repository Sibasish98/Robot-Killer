using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class arenaSelector : MonoBehaviour
{
    public Image arenaThumbnail;
    public TMP_Text title;
    public TMP_Text arenaName;
    public TMP_Text arenaScrollCount;

    public int currentSelectedArena; //this variable should be the variable to be read to determine which arena is selected in the window
    public int scrollValue = 0;

    public Button nextBtn, prevBtn;
    public List<Color> arenaThumbnails;
    public List<string> arenaNames;
    int maxArena;
    // Start is called before the first frame update
    void Start()
    {
        title.text = "Choose Arena";
        maxArena = arenaNames.Count;
        arenaName.text = arenaNames[0];
        arenaThumbnail.color = arenaThumbnails[0];
        setScrollText();
        currentSelectedArena = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onNextButtonClicked()
    {
        scrollValue++;

        currentSelectedArena = scrollValue;

        arenaThumbnail.color = arenaThumbnails[scrollValue];
        arenaName.text = arenaNames[scrollValue];
        if (scrollValue == maxArena)
        {
            //if the list is at end we dont need the next  button
            nextBtn.gameObject.SetActive(false);
        }
        if (scrollValue == 1)
        {
            prevBtn.gameObject.SetActive(true);
        }
        setScrollText();
    }
    public void onPreviousButtonClicked()
    {
        scrollValue--;
        currentSelectedArena = scrollValue;
        arenaThumbnail.color = arenaThumbnails[scrollValue];
        arenaName.text = arenaNames[scrollValue];
        if (scrollValue == 1)
        {
            //if the list is at begining we dont need the previous button
            prevBtn.gameObject.SetActive(false);
        }
        if (scrollValue == maxArena-1)
        {
            nextBtn.gameObject.SetActive(true);
        }
        setScrollText();
    }
    public void setScrollText()
    {
        arenaScrollCount.text = (scrollValue+1) + "/" + maxArena;
    }
}
