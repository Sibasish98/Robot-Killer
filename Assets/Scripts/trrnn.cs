using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trrnn : MonoBehaviour
{
    public TerrainData td;
    // Start is called before the first frame update
    void Start()
    {
       // Debug.Log(td.GetHeight(0, 0));
        float[,] a = new float[513,513];
        float[,,] b = new float[512, 512,2];
        float high = td.GetHeight(0, 0);
        for (int i=0;i<513;i++)
        {
            for (int j=0;j<513;j++)
            {
                if (i > 100 && j > 100)
                    a[i, j] = 5.23f;
                else
                    a[i, j] = 0f;
                float t = td.GetHeight(i, j);
                if (t > high)
                    high = t;
            }
        }
        //for texture
        for (int i = 0; i < 512; i++)
        {
            for (int j = 0; j < 512; j++)
            {
                if (i > 100 && j > 100)
                    b[i, j,1] = 1f;
                else
                    b[i, j,0] = 1f;
            }
        }
        td.SetHeights(0, 0, a);
        td.SetAlphamaps(0, 0, b);
        float[,,] c = td.GetAlphamaps(0, 0, 512, 512);
        Debug.Log(a.GetLength(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
