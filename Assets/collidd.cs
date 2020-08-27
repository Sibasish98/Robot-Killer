using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidd : MonoBehaviour
{
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        Texture2D t = sr.sprite.texture;
        
        int rows = t.width;
        int columns = t.height;
        for (int i=0;i<rows; i++)
        {
            for (int j=0;j<columns;j++)
            {
                Color temp = t.GetPixel(i, j);
                temp.a = 0;
                t.SetPixel(i, j, temp);
            }
        }
        t.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
