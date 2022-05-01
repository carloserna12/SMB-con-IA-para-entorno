using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;
public class MapMaker : MonoBehaviour
{

    public Tile tile;
    public Tilemap tilemap;
    
    public Transform prefab1;
    public Transform prefab2;
    public Transform prefab3;
    public Transform prefab4;
    public int mapWidth;
    public int mapHeight;

    private int[,] mapData;

    private int prueb;
    private string obstaculo = "";
    public List<int> updateData = new List<int>();
    public List<int> pochoclo = new List<int>();

    private GameStateManager t_GameStateManager;

    // Start is called before the first frame update
    void Start()
    {
        t_GameStateManager = FindObjectOfType<GameStateManager> ();
        this.mapData = new int[this.mapWidth,this.mapHeight];
        prueb = 0;
        int aux = 0;




        for (int i = 0; i < this.mapWidth; i++)
        {
            for (int j = 0; j < this.mapHeight; j++)
            {
                
               
                /*
                int ranNum= Random.Range(0, 70);
                if (ranNum == 1 || ranNum == 2 || ranNum == 3 ||ranNum == 4)
                {
                    this.mapData[i,j] = ranNum;
                }else
                {
                    this.mapData[i,j] = 0;
                }
                */

                
                if(i == 23  && j == 5 ){
                    this.mapData[i,j] = 1;
                }else {
                    this.mapData[i,j] = 0;
                }
                

                updateData.Add(this.mapData[i,j]);
                /*
                if(prueb < 6)
                {
                    this.mapData[i,j] = 0;
                    prueb++;
                }else if(prueb==6)
                {
                    this.mapData[i,j] = 1;
                    prueb=0;
                }
                */
            }
        }
        
        

        this.GenerateTiles();

        string str = string.Join(",",updateData);
        Debug.Log(str);
        
    }

    void extraerCoordenadas(string coor)
    {
        if (coor == "")
        {
            Debug.Log("mosquitoshptas");
        }else
        {
            if(coor[4].Equals('|') && coor[6].Equals(']')){
                string a = Convert.ToString(coor[3]);
                string b = Convert.ToString(coor[5]);
                pochoclo.Add(Int16.Parse(a));
                pochoclo.Add(Int16.Parse(b));
            }else if (coor[5].Equals('|') && coor[7].Equals(']'))
            {
                string a = Convert.ToString(coor[3]);
                string b = Convert.ToString(coor[4]);
                string c = Convert.ToString(coor[6]);
                pochoclo.Add(Int16.Parse(a+b));
                pochoclo.Add(Int16.Parse(c));
            }
        }
    }
    //Pinta los obstaculos en el tilemap
    void GenerateTiles()
    {
        for (int i = 0; i < this.mapWidth; i++)
        {
            for (int j = 0; j < this.mapHeight; j++)
            {
                if(this.mapData[i,j] == 1)
                {
                   /* this.tilemap.SetTile(
                        new Vector3Int(i,j,0),
                        this.tile
                    );*/
                    Instantiate(prefab1, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.mapData[i,j]==2)
                {
                    Instantiate(prefab2, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.mapData[i,j]==3)
                {
                    Instantiate(prefab3, new Vector3(i, j, 0), Quaternion.identity);
                }else if (this.mapData[i,j]==4)
                {
                    Instantiate(prefab4, new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
