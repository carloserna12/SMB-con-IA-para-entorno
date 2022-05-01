using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellularData : MonoBehaviour
{
    public float fillPercent = 0.5f;

    public int interations = 1;


    public int[,] GenerateData(int w, int h)
    {
        int[,] mapData = new int[w,h];
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                float chance = Random.Range(0f, 1f);

                mapData[i,j] = chance < this.fillPercent ? 1 : 0;
            }
        }

        int[,] buffer = new int[w,h];



        return mapData;
    }
}
